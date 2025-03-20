# HomeAssignment2


### Login Credentials

#### Students

- john123 : john123
- jane123 : jane123
#### Teachers

- smith123 : smith123
- nora123 : nora123


## Splitting the work

- Annalena: 
    - Login view
    - set up JSON
    - set up tests
    - hashing
    - merging
- Gabija:
    - Student view
    - set up tests
    - merging
- Emma:
    - Teacher view  
    - set up tests   
    - merging   


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