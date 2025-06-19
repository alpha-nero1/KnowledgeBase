# CURLs
`curl` is a command-line tool used to transfer data using various network protocols, most commonly HTTP and HTTPS. This guide will help you understand how to use `curl` on both Windows and macOS.

## 📦 Basic Usage

### GET Request
```bash
curl https://www.google.com
```

### POST Request
```bash
curl -X POST https://api.example.com/submit -d "key1=value1&key2=value2"
```

### Add Headers
```bash
curl -H "Authorization: Bearer <token>" https://api.example.com/protected
```

### Send JSON Data
```bash
curl -X POST https://api.example.com/json \
  -H "Content-Type: application/json" \
  -d '{"name": "Alé", "age": 27}'
```

### Save Response to a File
```bash
curl https://example.com/file.zip -o file.zip
```

---

## 💻 Platform-Specific Notes

### macOS (Terminal)
- Uses standard Unix-style quoting.
- Wrap JSON or special characters in **single quotes** (`'`) to avoid shell interference.
- Example:
  ```bash
  curl -X POST https://api.com -d '{"key":"value"}'
  ```

### Windows (Command Prompt or PowerShell)
- Use **double quotes** (`"`) and escape double quotes inside the JSON with `\"`.
- Example:
  ```cmd
  curl -X POST https://api.com -d "{\"key\":\"value\"}"
  ```

> ✅ **Tip**: PowerShell may also interpret quotes differently; always test with simple inputs first.

---

## 🌐 Common Flags

| Flag           | Description                             |
|----------------|-----------------------------------------|
| `-X`           | Specify request method (GET, POST, etc.)|
| `-d`           | Send data in POST or PUT requests       |
| `-H`           | Add custom header(s)                    |
| `-o`           | Output response to a file               |
| `-L`           | Follow redirects                        |
| `-u`           | Basic authentication (`-u user:pass`)   |

---

## 🧪 Example: Uploading a File

```bash
curl -X POST https://api.example.com/upload \
  -F "file=@/path/to/your/file.txt"
```

On Windows:
```cmd
curl -X POST https://api.example.com/upload -F "file=@C:\path\to\file.txt"
```

---

## 🆘 Help

To view all available options, run:

```bash
curl --help
```

Or refer to the [official curl documentation](https://curl.se/docs/manpage.html).

---

## 🔐 Security Note
Avoid exposing sensitive information like API tokens or passwords in your terminal history. Consider using environment variables or `.env` files for secure handling.
