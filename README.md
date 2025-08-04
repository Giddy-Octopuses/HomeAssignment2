# Advanced OOP – Home Assignment 2

- University Management Application
- Assignment Duration: 2 weeks
- Contributors: Annalena, Emma, Gabija

### Login Credentials

#### Students

- john123 : john123
- jane123 : jane123
#### Teachers

- smith123 : smith123
- nora123 : nora123

### Functional Requirements

#### Student Use Cases
- Enroll in a Subject 
  
    -- Subject moves to Enrolled Subjects

    --- Confirmation message shown

- Drop a Subject 
  
    -- Subject returns to Available Subjects

    --- Confirmation message shown

#### Teacher Use Cases
- Create a Subject 
  
    -- Subject appears in both My Subjects and Available Subjects
  
    --- Confirmation message shown
  
- Delete a Subject 
  
    -- Subject removed from both My Subjects and Available Subjects
  
    --- Confirmation message shown

##### Functional Testing Documentation

- TeacherViewModel Tests -

    -- Create Subject
        Adds a new subject.
        Verifies the subject appears in both "My Subjects" and "Available Subjects"

    -- Delete Subject:
        Removes a subject.
        Verifies the subject is removed from both "My Subjects" and "Available Subjects"

- StudentViewModel Tests -

    -- Enroll Subject
        Enrolls a student in a subject.
        Verifies the subject moves from "Available Subjects" to "Enrolled Subjects"

    -- Drop Subject
        Drops a subject.
        Verifies the subject moves back to "Available Subjects"

- LoginView Tests -

    -- Valid Login
        Tests student and teacher login with correct credentials.

    -- Invalid Login
        Tests login with wrong credentials or mismatched roles.
        Verifies error message.

    -- Empty Fields
        Tests login with missing username or password.
        Verifies error prompt.

- DataRepository Tests -

    -- Load Data
        Verifies data loading from a JSON file.

    -- Save Data
        Verifies saving data back to a JSON file.
