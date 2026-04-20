#!/usr/bin/env bash
# ---------------------------------------------------------------------------
# deploy_to_hf.sh
# Copies the necessary project files into the vocabbot/ HuggingFace repo
# folder, ready for git commit and push.
#
# Usage: ./deploy_to_hf.sh
# ---------------------------------------------------------------------------

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
HF_DIR="$SCRIPT_DIR/vocabbot"

echo "▶ Syncing files into $HF_DIR ..."

# --- Python source files ---
cp "$SCRIPT_DIR/app.py" "$HF_DIR/app.py"

mkdir -p "$HF_DIR/lib"
cp "$SCRIPT_DIR/lib/vocab_agent.py" "$HF_DIR/lib/vocab_agent.py"

# Keep lib importable as a package
touch "$HF_DIR/lib/__init__.py"

# --- Dependency file (HF Spaces uses pip, not uv) ---
cat > "$HF_DIR/requirements.txt" <<'EOF'
gradio>=4.44.0
openai>=1.50.0
python-dotenv>=1.0.1
EOF

# --- Env example (never copy the real .env) ---
cp "$SCRIPT_DIR/.env.example" "$HF_DIR/.env.example"

# --- Ensure .env is gitignored inside the HF repo ---
GITIGNORE="$HF_DIR/.gitignore"
if ! grep -qxF ".env" "$GITIGNORE" 2>/dev/null; then
    echo ".env" >> "$GITIGNORE"
    echo "  added .env to $GITIGNORE"
fi

echo ""
echo "✅ Done. Files copied:"
echo "   vocabbot/app.py"
echo "   vocabbot/lib/vocab_agent.py"
echo "   vocabbot/lib/__init__.py"
echo "   vocabbot/requirements.txt"
echo "   vocabbot/.env.example"
echo ""
echo "⚠️  Set your OPENAI_API_KEY as a Space secret in the HuggingFace UI."
echo "   Do NOT commit your .env file."
echo ""
echo "Next steps:"
echo "  cd vocabbot"
echo "  git add ."
echo "  git commit -m 'deploy vocabbot'"
echo "  git push"
