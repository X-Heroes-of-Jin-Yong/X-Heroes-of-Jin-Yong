#!/bin/bash
# ============================================
# Template Mod DLL 编译脚本
# 运行: cd gamedata/modcache/Template && bash build.sh
# 输出: Mod.dll
# ============================================
set -e
cd "$(dirname "$0")"

echo "Compiling ModEntry.cs → Mod.dll ..."

dotnet build ModEntry.csproj -c Release -o . --nologo -v q 2>&1

if [ -f "Mod.dll" ]; then
    # 清理中间文件
    rm -f Mod.pdb Mod.deps.json Mod.csproj.nuget.* 2>/dev/null
    rm -rf obj bin 2>/dev/null

    echo ""
    echo "============================================"
    echo "  ✓ 编译成功"
    echo "  文件: $(realpath Mod.dll)"
    echo "  大小: $(stat -c%s Mod.dll 2>/dev/null || stat -f%z Mod.dll) bytes"
    echo ""
    echo "  Mod 已就绪，重启游戏即可加载。"
    echo "============================================"
else
    echo "✗ 编译失败，请检查上方错误。"
    exit 1
fi
