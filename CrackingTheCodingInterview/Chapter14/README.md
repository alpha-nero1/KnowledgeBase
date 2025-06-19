# SQL

## GROUP BY
A `GROUP BY` statement in SQL tells the DB to partition result rows into groups based on columns specified. Grouping is
typically performed to get an aggregated result from the group (like count or sum). The result of a query using a GROUP BY
statement contains one row for each group.

E.g.

```
-- Getting the count of how many courses each teacher teaches.
SELECT TeacherID, COUNT(StudentCourses.CourseID)
    FROM Courses
    INNER JOIN StudentCourses ON Courses.CourseID = StudentCourses.CourseID 
    GROUP BY Courses.TeacherID;
```

## HAVING
A `HAVING` statement specifies a search condition for the result of a `GROUP BY` clause (aggregate).
We can use it in congunction with `GROUP BY` to only get certain groups from the query...

E.g.

```
-- Getting the count of how many courses teachers who teach more than 1 course teach.
SELECT TeacherID, COUNT(StudentCourses.CourseID)
    FROM Courses
    INNER JOIN StudentCourses ON Courses.CourseID = StudentCourses.CourseID 
    GROUP BY Courses.TeacherID
    HAVING COUNT(StudentCourses.CourseID) > 1;
```