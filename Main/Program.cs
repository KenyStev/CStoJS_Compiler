
using System.Collections.Generic;
using System.IO;
using System.Text;
using Compiler;

namespace Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var inputString = new InputString(@"print IDTEST = 5595 + ( TRE ) - QUATTRO * UNO//Hola IDDUE/ALGO;");

            // var inputString = new InputString(@"int hola string adios como_0101 
            // 123 12 0x2bc3a 0b0100 0X5d6a3 0B01110 8 0 485hola
            // 123f 12F 8.58f 0.05F
            // true false
            // @""Hola como """"estan todos""""   
            // nada \mas\ que 'hacer',....."" @""otra ves{](0)""");

//             var inputString = new InputString(@"
// int a  10

// int b  20
// 22							

// 0xAF2323F5					
// 0b00100010					
// 51.37f						
// 51.37F						
// true						
// false						
// 'c'									
// hello					
// ");

            var inputString = new InputFile(@"..\Lexer.Tests\TokenTypeTests.cs");
            var tokenGenerators = getTokenGenerators();

            var lexer = new Lexer(inputString, tokenGenerators);

            Token token = lexer.GetNextToken();

            while (token.type != TokenType.EOF)
            {
                System.Console.Out.WriteLine(token);
                token = lexer.GetNextToken();
            }

            System.Console.Out.WriteLine(token);

            // System.Console.ReadKey();

            // var br = new BinaryReader(File.Open(@"..\literals.txt",FileMode.Open,FileAccess.Read) ,Encoding.UTF8,true);
            // var nextChar = br.ReadChar();
            // // while(br.BaseStream.Position < br.BaseStream.Length)
            // {
            //     br.BaseStream.Seek(-2,SeekOrigin.End);
            //     System.Console.WriteLine(nextChar);
            //     nextChar = (char)br.PeekChar();
            //     System.Console.WriteLine(nextChar);
            //     br.BaseStream.Seek(-1,SeekOrigin.End);
            //     nextChar = br.ReadChar();
            //     System.Console.WriteLine(nextChar);
            // }
        }

        private static List<ITokenGenerator> getTokenGenerators()
        {
            var tokenGenerators = new List<ITokenGenerator>();
            
            tokenGenerators.Add(new EOFTokenGenerator());
            tokenGenerators.Add(new IDReservedWordTokenGenerator());
            tokenGenerators.Add(new LiteralIntTokenGenerator());
            tokenGenerators.Add(new LiteralCharTokenGenerator());
            tokenGenerators.Add(new LiteralStringTokenGenerator());
            tokenGenerators.Add(new OperatorsTokenGenerator());
            tokenGenerators.Add(new PuntuationTokenGenerator());

            return tokenGenerators;
        }
    }
}
