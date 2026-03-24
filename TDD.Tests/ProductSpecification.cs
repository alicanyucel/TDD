using FluentAssertions;

namespace TDD.Tests;

public class ProductSpecification
{
    [Fact]
    //önce metod ismi  sonra ne olması gerektiği exception ne zaman
    public async Task Create_Should_Be_Throw_Argument_Exception_If_Name_Less_Than_3_Characters ()
    {
        /* Tdd cycle
         1.Önce başarısz test yaz
         testteki tüm clas v metotları olusuuyoruz
        2)testi başarılı kıl
        3)Refactor ile kodu iyileştiriyoruz
         
         */
        //Arrange öncelikle tanımalanan class
        CreateProductCommand request = new ("La",1);
        CreateProductCommandHandler commnad = new();

        //Act işlem burada
        var action = async ()=> await commnad.Handle(request, default);

        //Assert işlem sonucnun testi işlem sonucu bu olmalı
        (await action.Should().ThrowAsync<ProductNameNotValidException>()).WithMessage("Name must be at least 3 characters long.");
    }
 2   [Fact]
    public async Task Create_Should_Be_Throw_Argumenet_Exception_If_Price_Equal_Or_Lower_Than_0()
    {
        //arrange 
        CreateProductCommand request = new("Laptop", 0);
        CreateProductCommandHandler commnad = new();

        //act
        var action=async()=>await commnad.Handle(request, default);


        //Assert
        await action.Should().ThrowAsync<ProductPriceNotValidException>();
        
    }
}
public sealed record CreateProductCommand(string Name,decimal Price);
public sealed class  CreateProductCommandHandler
{
    public async Task Handle(CreateProductCommand request,CancellationToken cancellationToken)
    {
        if (request.Name.Length < 3)
        {
            throw new ProductNameNotValidException();
        }

        if (request.Price <= 0)
        {
            throw new ProductPriceNotValidException();
        }

        await Task.CompletedTask;
    }
}
public sealed class ProductNameNotValidException : Exception
{
    public ProductNameNotValidException():base("Name must be at least 3 characters long.")
    {
        
    }
}
    public sealed class ProductPriceNotValidException : Exception
    {
        public ProductPriceNotValidException():base("Price must be greater than 0")
        {
            
        }
    }
