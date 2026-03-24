using FluentAssertions;

namespace TDD.Tests;

public class ProductSpecification
{
    [Fact]
    //önce metod ismi  sonra ne olması gerektiği exception ne zaman
    public async Task Create_Should_Be_Throw_Exception_If_Name_Less_Than_3_Characters ()
    {

        //Arrange öncelikle tanımalanan class
        CreateProductCommand request = new ("La");
        CreateProductCommandHandler commnad = new();

        //Act işlem burada
        var action = async ()=> await commnad.Handle(request, CancellationToken.None);


        //Assert işlem sonucnun testi işlem sonucu bu olmalı
         await  action.Should().ThrowAsync<Exception>();
    }
}
public sealed record CreateProductCommand(string Name);
public sealed class  CreateProductCommandHandler
{
    public async Task Handle(CreateProductCommand command,CancellationToken cancellationToken)
    {
        throw new Exception("Name must be at least 3 characters long.");
        await Task.CompletedTask;
    }
}