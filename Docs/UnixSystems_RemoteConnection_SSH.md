# SSH
## How to generate an SSH private/public key pair on your local unix based machine.

This guide covers how to create SSH keys on macOS/Linux, add them to your agent,
register them with providers like GitHub/GitLab, and troubleshoot common issues.

## 1) Check if you already have SSH keys

```bash
ls -al ~/.ssh
```

Look for existing public keys such as:
- `id_ed25519.pub`
- `id_rsa.pub`

If you already have a key and want to reuse it, skip to section 3.

## 2) Generate a new key pair (recommended: ed25519)

```bash
ssh-keygen -t ed25519 -C "your_email@example.com"
```

Option meanings:
- `-t ed25519`: key type to generate (`ed25519`)
- `-C "..."`: comment to attach to the key (commonly your email)

When prompted:
- Press Enter to accept default path (`~/.ssh/id_ed25519`)
- Enter a secure passphrase (recommended)

If `ed25519` is unavailable on an older system:

```bash
ssh-keygen -t rsa -b 4096 -C "your_email@example.com"
```

Option meanings:
- `-t rsa`: key type to generate (`rsa`)
- `-b 4096`: key size in bits
- `-C "..."`: comment to attach to the key

## 3) Start ssh-agent and add your private key

```bash
eval "$(ssh-agent -s)"
ssh-add ~/.ssh/id_ed25519
```

Option meanings:
- `ssh-agent -s`: output shell commands for a POSIX shell to set agent environment variables

For RSA keys:

```bash
ssh-add ~/.ssh/id_rsa
```

## 4) Copy your public key

macOS:

```bash
pbcopy < ~/.ssh/id_ed25519.pub
```

Linux (if `xclip` is installed):

```bash
xclip -sel clip < ~/.ssh/id_ed25519.pub
```

Option meanings:
- `-sel clip`: use the clipboard selection (not primary selection)

Fallback:

```bash
cat ~/.ssh/id_ed25519.pub
```

Copy the output manually if clipboard tools are unavailable.

## 5) Add key to your Git provider

### GitHub
1. Go to `Settings -> SSH and GPG keys -> New SSH key`
2. Paste your public key
3. Save

### GitLab
1. Go to `Preferences -> SSH Keys`
2. Paste your public key
3. Save

### Bitbucket
1. Go to `Personal settings -> SSH keys -> Add key`
2. Paste your public key
3. Save

## 6) Test SSH authentication

GitHub:

```bash
ssh -T git@github.com
```

Option meanings:
- `-T`: disable pseudo-terminal allocation (useful for Git SSH auth checks)

GitLab:

```bash
ssh -T git@gitlab.com
```

Option meanings:
- `-T`: disable pseudo-terminal allocation

Bitbucket:

```bash
ssh -T git@bitbucket.org
```

Option meanings:
- `-T`: disable pseudo-terminal allocation

Expected result should confirm successful authentication.

## 7) Optional: configure SSH for multiple keys/accounts

Create or edit `~/.ssh/config`:

```sshconfig
Host github-personal
	HostName github.com
	User git
	IdentityFile ~/.ssh/id_ed25519_personal
	AddKeysToAgent yes
	UseKeychain yes

Host github-work
	HostName github.com
	User git
	IdentityFile ~/.ssh/id_ed25519_work
	AddKeysToAgent yes
	UseKeychain yes
```

Then use the host alias in remotes, for example:

```bash
git remote set-url origin git@github-work:org/repo.git
```

## 8) Recommended permissions

```bash
chmod 700 ~/.ssh
chmod 600 ~/.ssh/id_ed25519
chmod 644 ~/.ssh/id_ed25519.pub
chmod 600 ~/.ssh/config
```

## 9) Common troubleshooting

### "Permission denied (publickey)"
- Ensure key is loaded: `ssh-add -l`
- Ensure correct key is used in `~/.ssh/config`
- Ensure the public key is added to your provider account

Option meaning:
- `ssh-add -l`: `-l` lists fingerprints of identities currently loaded in `ssh-agent`

### "Host key verification failed"
- Check/clean old host entries:
	```bash
	ssh-keygen -R github.com
	```
	
	Option meaning:
	- `-R github.com`: remove all keys for `github.com` from `known_hosts`
- Retry connection

### Debug connection

```bash
ssh -vT git@github.com
```
or 
```bash
ssh -vT git@bitbucket.com
```

Option meanings:
- `-v`: verbose output for connection debugging
- `-T`: disable pseudo-terminal allocation

Use `-vv` or `-vvv` for more detail.

## 10) Quick reference

Generate key:

```bash
ssh-keygen -t ed25519 -C "your_email@example.com"
```

Add key to agent:

```bash
eval "$(ssh-agent -s)" && ssh-add ~/.ssh/id_ed25519
```

Test GitHub auth:

```bash
ssh -T git@github.com
```
