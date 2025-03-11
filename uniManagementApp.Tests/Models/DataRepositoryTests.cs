using uniManagementApp.Models;

namespace uniManagementApp.Tests;

public class DataRepositoryTests
{
    [Fact]
    public void Data_Exists()
    {
        string curFile = "../../uniManagementApp/Data/data.json";
        Assert.True(File.Exists(curFile), "data.json file does not exist.");
    }

    [Fact]
    public void DataRepository_Constructor()
    {
        // Arrange
        var dataRepository = new DataRepository();
        // Assert
        Assert.NotNull(dataRepository.Subjects);
        Assert.NotNull(dataRepository.Students);
        Assert.NotNull(dataRepository.Teachers);
        
        Assert.True(dataRepository.Subjects.Any(), "Subjects collection should not be empty.");
        Assert.True(dataRepository.Students.Any(), "Students collection should not be empty.");
        Assert.True(dataRepository.Teachers.Any(), "Teachers collection should not be empty.");

    }
}
