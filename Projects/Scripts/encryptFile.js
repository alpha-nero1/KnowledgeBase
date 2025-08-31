/**
 * Security and encryption utilities
 * Useful for: Secure file handling, API security, data protection
 */

/**
 * Creates a secure token generator with configurable defaults
 */
const createTokenGenerator = (defaultLength = 32) => 
    (length = defaultLength) => 
        crypto.randomBytes(length).toString('hex');

/**
 * Creates a password hasher with configurable parameters
 */
const createPasswordHasher = (iterations = 10000, keyLength = 64, digest = 'sha512') => {
    // Private helper to generate salt
    const generateSalt = (length = 16) => {
        return crypto.randomBytes(length).toString('hex');
    };

    const hashPassword = (password, salt = null) => {
        if (!salt) salt = generateSalt();
        const hash = crypto.pbkdf2Sync(password, salt, iterations, keyLength, digest).toString('hex');
        return { hash: `${salt}:${hash}`, salt };
    };

    const verifyPassword = (password, storedHash) => {
        const [salt, hash] = storedHash.split(':');
        const verifyHash = crypto.pbkdf2Sync(password, salt, iterations, keyLength, digest).toString('hex');
        return hash === verifyHash;
    };

    return {
        hashPassword,
        verifyPassword,
        generateSalt: () => generateSalt()
    };
};

/**
 * Creates a file encryptor with configurable encryption settings
 */
const createFileEncryptor = (algorithm = 'aes-256-gcm', iterations = 10000, keyLength = 32) => {
    // Private helper to derive key from password
    const deriveKey = (password, salt) => crypto.pbkdf2Sync(
        password, 
        salt, 
        iterations, 
        keyLength, 
        'sha512'
    );

    // Private helper to generate secure random bytes
    const generateSecureBytes = (length) => crypto.randomBytes(length);

    return async (filePath, password) => {
        try {
            const salt = generateSecureBytes(16);
            const key = deriveKey(password, salt);
            const iv = generateSecureBytes(16);
            
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
            return encryptedPath;
        } catch (error) {
            throw error;
        }
    };
};

/**
 * Factory function that creates a complete security utilities object
 */
const SecurityUtils = (config = {}) => {
    const {
        defaultTokenLength = 32,
        passwordIterations = 10000,
        passwordKeyLength = 64,
        passwordDigest = 'sha512',
        encryptionAlgorithm = 'aes-256-gcm',
        encryptionIterations = 10000,
        encryptionKeyLength = 32
    } = config;

    // Create specialized function instances with configuration
    const tokenGenerator = createTokenGenerator(defaultTokenLength);
    const passwordHasher = createPasswordHasher(passwordIterations, passwordKeyLength, passwordDigest);
    const fileEncryptor = createFileEncryptor(encryptionAlgorithm, encryptionIterations, encryptionKeyLength);

    return {
        // Token generation
        generateSecureToken: tokenGenerator,
        
        // Password utilities
        hashPassword: passwordHasher.hashPassword,
        verifyPassword: passwordHasher.verifyPassword,
        generateSalt: passwordHasher.generateSalt,
        
        // File encryption
        encryptFile: fileEncryptor,
        
        // Configuration access
        getConfig: () => ({ ...config }),
        updateConfig: (newConfig) => {
            Object.assign(config, newConfig);
            return createSecurityUtils(config);
        }
    };
};

// Export individual utility functions for standalone use
const generateSecureToken = createTokenGenerator();
const { hashPassword, verifyPassword } = createPasswordHasher();
const encryptFile = SecurityUtils();