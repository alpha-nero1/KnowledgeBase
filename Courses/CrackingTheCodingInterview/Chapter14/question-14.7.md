# 14.7. Design Grade Database
Imagine a simple database storing information for students' grades.
Design what this database might look like and provide a SQL query to return a list of the honor roll
students (top 10%), sorted by their grade point average.

## Tables

### Students
+ StudentId
+ FirstName
+ LastName
+ DateOfBirth
+ CreatedAt
+ UpdatedAt
+ DisabledAt

### StudentSubjects
+ StudentSubjectId
+ StudentId
+ SubjectId
+ CreatedAt
+ UpdatedAt
+ DisabledAt

### Subject
+ SubjectId
+ Name
+ CreatedAt
+ UpdatedAt
+ DisabledAt

### Grades
+ GradeId
+ StudentSubjectId
+ Name
+ Grade
+ CreatedAt
+ UpdatedAt
+ DisabledAt

```
SELECT * FROM "Students" st LEFT JOIN (
    SELECT "StudentId", AVG(gs.Grade) as "GradePointAverage" FROM "Students" ist
    LEFT JOIN "StudentSubjects" istsbs ON istsbs."StudentId" = ist."StudentId"
    LEFT JOIN "Grades" igs ON gs."StudentSubjectId" = istsbs."StudentSubjectId"
    GROUP BY ist."StudentId"
) as "StudentGradePointAverages"
ON "StudentGradePointAverages"."StudentId" = st."StudentId"
ORDER BY "StudentGradePointAverages"."GradePointAverage" DESC;
```