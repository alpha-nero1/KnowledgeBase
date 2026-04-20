# Java & Maven
In this readme please scaffold out a lesson plan for me to learn the funadamentals of Java & Maven.

The lesson should have steps to follow, and should have the following objectives
1 - Set up simple APIs
2 - Set up business logic
3 - Set up data access connecting to a sqlite DB
4 - Demonstrate inter package dependencies and shared utilities

# Java & Maven
In this readme please scaffold out a lesson plan for me to learn the funadamentals of Java & Maven.

The lesson should have steps to follow, and should have the following objectives
1 - Set up simple APIs
2 - Set up business logic
3 - Set up data access connecting to a sqlite DB
4 - Demonstrate inter package dependencies and shared utilities

## Lesson Plan

### Objective 1: Set up Simple APIs
- Create a Maven project with Spring Boot starter.
```
brew install maven
brew tap spring-io/tap
brew install spring-boot
spring init -d=web,jpa --build=maven --java-version=21 my-data-api
```
- Add REST controller to expose simple endpoints (e.g., GET /hello, POST /user).
- Use annotations like @RestController, @GetMapping, @PostMapping.

### Objective 2: Set up Business Logic
- Create service classes to handle business logic.
- Implement methods for data processing, validation, and rules.
- Inject services into controllers using @Autowired.

### Objective 3: Set up Data Access Connecting to a SQLite DB
- Add H2 or SQLite dependency (use H2 for in-memory SQLite-like DB).
- Create entity classes with JPA annotations.
- Implement repository interfaces using Spring Data JPA.
- Configure application.properties for database connection.

### Objective 4: Demonstrate Inter-Package Dependencies and Shared Utilities
- Organize code into packages: controller, service, repository, model, util.
- Create shared utility classes (e.g., for string manipulation, date formatting).
- Show how packages depend on each other (e.g., controller -> service -> repository).
- Use Maven modules if advanced, but for fundamentals, keep in one module.

### Steps to Follow:
1. **Initialize Maven Project:**
   - Use `mvn archetype:generate` to create a quickstart project.
   - Or use Spring Initializr for Spring Boot project.

2. **Add Dependencies:**
   - Spring Boot Starter Web
   - Spring Boot Starter Data JPA
   - H2 Database (for SQLite-like functionality)

3. **Create Package Structure:**
   - com.example.project
     - controller
     - service
     - repository
     - model
     - util

4. **Implement Models:**
   - Create User.java with fields like id, name, email.

5. **Implement Repositories:**
   - Create UserRepository interface extending JpaRepository.

6. **Implement Services:**
   - Create UserService with methods like saveUser, getAllUsers.

7. **Implement Controllers:**
   - Create UserController with endpoints to interact with UserService.

8. **Add Utilities:**
   - Create a utility class for common functions, e.g., StringUtils.

9. **Run and Test:**
   - Use `mvn spring-boot:run` to start the application.
   - Test endpoints with curl or Postman.

10. **Demonstrate Dependencies:**
    - Show how controller depends on service, service on repository, and all use utils.
