"""
    Using direct tools instead of MCP server for simplicity.
"""

from __future__ import annotations

from pathlib import Path
import subprocess
import sys
from typing import Optional, Sequence
from agents import function_tool

WORKSPACE_ROOT = Path(__file__).resolve().parent.parent
OUTPUT_ROOT = WORKSPACE_ROOT / "output"

def _resolve_path(path: str) -> Path:
    resolved = (OUTPUT_ROOT / path).resolve()
    if not resolved.is_relative_to(OUTPUT_ROOT):
        raise ValueError("Path must be within the output folder")
    return resolved


@function_tool
def create_file(path: str, content: str) -> str:
    """Create a new file under /output. Fails if the file exists."""
    file_path = _resolve_path(path)
    if file_path.exists():
        raise ValueError(f"File already exists: {path}")
    file_path.parent.mkdir(parents=True, exist_ok=True)
    file_path.write_text(content, encoding="utf-8")
    return f"Created {path}"


@function_tool
def overwrite_file(path: str, content: str) -> str:
    """Overwrite a file under /output (creates it if missing)."""
    file_path = _resolve_path(path)
    file_path.parent.mkdir(parents=True, exist_ok=True)
    file_path.write_text(content, encoding="utf-8")
    return f"Wrote {path}"


@function_tool
def append_file_text(path: str, content: str) -> str:
    """Append text to a file under /output (creates it if missing)."""
    file_path = _resolve_path(path)
    file_path.parent.mkdir(parents=True, exist_ok=True)
    with file_path.open("a", encoding="utf-8") as handle:
        handle.write(content)
    return f"Appended to {path}"


@function_tool
def read_file_text(path: str, start_line: int = 1, end_line: Optional[int] = None) -> str:
    """Read file text under /output, optionally by line range."""
    file_path = _resolve_path(path)
    if not file_path.exists():
        raise ValueError(f"File does not exist: {path}")
    lines = file_path.read_text(encoding="utf-8").splitlines()
    if start_line < 1:
        raise ValueError("start_line must be >= 1")
    if end_line is None:
        end_line = len(lines)
    if end_line < start_line:
        raise ValueError("end_line must be >= start_line")
    sliced = lines[start_line - 1 : end_line]
    return "\n".join(sliced)


@function_tool
def replace_text_in_file(path: str, old: str, new: str, count: int = 1) -> str:
    """Replace text in a file under /output. Defaults to first occurrence."""
    file_path = _resolve_path(path)
    if not file_path.exists():
        raise ValueError(f"File does not exist: {path}")
    content = file_path.read_text(encoding="utf-8")
    if old not in content:
        raise ValueError("Target text not found")
    updated = content.replace(old, new, count)
    file_path.write_text(updated, encoding="utf-8")
    return f"Updated {path}"


@function_tool
def run_python_file(path: str, args: Optional[Sequence[str]] = None, timeout_seconds: int = 30) -> str:
    """Run a Python file under /output and return stdout/stderr."""
    file_path = _resolve_path(path)
    if not file_path.exists():
        raise ValueError(f"File does not exist: {path}")
    if file_path.suffix != ".py":
        raise ValueError("Only .py files can be executed")
    cmd = [sys.executable, str(file_path)]
    if args:
        cmd.extend(args)
    result = subprocess.run(
        cmd,
        cwd=str(OUTPUT_ROOT),
        capture_output=True,
        text=True,
        timeout=timeout_seconds,
    )
    stdout = result.stdout.strip()
    stderr = result.stderr.strip()
    return (
        f"Exit code: {result.returncode}\n"
        f"STDOUT:\n{stdout}\n"
        f"STDERR:\n{stderr}"
    )


developer_tools = [
    create_file,
    overwrite_file,
    append_file_text,
    read_file_text,
    replace_text_in_file,
]

tester_tools = [
    *developer_tools,
    run_python_file,
]