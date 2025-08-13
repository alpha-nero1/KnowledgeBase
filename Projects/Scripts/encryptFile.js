// =====================================
// 6. 🔐 SECURITY & ENCRYPTION UTILITIES
// =====================================

/**
 * Security and encryption utilities
 * Useful for: Secure file handling, API security, data protection
 */
class SecurityUtils {
    static generateSecureToken(length = 32) {
        return crypto.randomBytes(length).toString('hex');
    }
    
    static hashPassword(password, salt = null) {
        if (!salt) salt = crypto.randomBytes(16).toString('hex');
        const hash = crypto.pbkdf2Sync(password, salt, 10000, 64, 'sha512').toString('hex');
        return { hash: `${salt}:${hash}`, salt };
    }
    
    static verifyPassword(password, storedHash) {
        const [salt, hash] = storedHash.split(':');
        const verifyHash = crypto.pbkdf2Sync(password, salt, 10000, 64, 'sha512').toString('hex');
        return hash === verifyHash;
    }
    
    static encryptFile(filePath, password) {
        return new Promise(async (resolve, reject) => {
            try {
                const algorithm = 'aes-256-gcm';
                const salt = crypto.randomBytes(16);
                const key = crypto.pbkdf2Sync(password, salt, 10000, 32, 'sha512');
                const iv = crypto.randomBytes(16);
                
                const cipher = crypto.createCipher(algorithm, key);
                cipher.setAAD(salt);
                
                const input = await fs.readFile(filePath);
                const encrypted = Buffer.concat([
                    cipher.update(input),
                    cipher.final()
                ]);
                
                const authTag = cipher.getAuthTag();
                
                const result = Buffer.concat([salt, iv, authTag, encrypted]);
                const encryptedPath = filePath + '.encrypted';
                
                await fs.writeFile(encryptedPath, result);
                
                console.log(`🔐 File encrypted: ${encryptedPath}`);
                resolve(encryptedPath);
            } catch (error) {
                reject(error);
            }
        });
    }
}