package com.xheroes.modimport;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;

import com.unity3d.player.UnityPlayer;

import java.io.File;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.UUID;

public final class ModFilePicker extends Activity {
    private static final int REQUEST_OPEN_DOCUMENT = 6407;
    private static final String EXTRA_OBJECT = "unityObject";
    private static final String EXTRA_METHOD = "unityMethod";

    public static void open(Activity unityActivity, String callbackObject, String callbackMethod) {
        Intent intent = new Intent(unityActivity, ModFilePicker.class);
        intent.putExtra(EXTRA_OBJECT, callbackObject);
        intent.putExtra(EXTRA_METHOD, callbackMethod);
        unityActivity.startActivity(intent);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Intent intent = new Intent(Intent.ACTION_OPEN_DOCUMENT);
        intent.addCategory(Intent.CATEGORY_OPENABLE);
        intent.setType("application/zip");
        intent.putExtra(Intent.EXTRA_MIME_TYPES, new String[] {
            "application/zip", "application/x-zip-compressed", "application/octet-stream"
        });
        startActivityForResult(intent, REQUEST_OPEN_DOCUMENT);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode != REQUEST_OPEN_DOCUMENT) {
            finish();
            return;
        }
        if (resultCode != RESULT_OK || data == null || data.getData() == null) {
            sendResult("");
            finish();
            return;
        }

        Uri uri = data.getData();
        new Thread(() -> copyAndReturn(uri), "ModSafImport").start();
    }

    private void copyAndReturn(Uri uri) {
        File destination = new File(getCacheDir(), "mod-import-" + UUID.randomUUID() + ".zip");
        try (InputStream input = getContentResolver().openInputStream(uri);
             OutputStream output = new FileOutputStream(destination)) {
            if (input == null) {
                throw new IllegalStateException("无法读取所选文件");
            }
            byte[] buffer = new byte[64 * 1024];
            int count;
            while ((count = input.read(buffer)) >= 0) {
                output.write(buffer, 0, count);
            }
            sendResult(destination.getAbsolutePath());
        } catch (Exception exception) {
            destination.delete();
            sendResult("ERROR:" + exception.getMessage());
        } finally {
            runOnUiThread(this::finish);
        }
    }

    private void sendResult(String result) {
        String callbackObject = getIntent().getStringExtra(EXTRA_OBJECT);
        String callbackMethod = getIntent().getStringExtra(EXTRA_METHOD);
        UnityPlayer.UnitySendMessage(callbackObject, callbackMethod, result == null ? "" : result);
    }
}
