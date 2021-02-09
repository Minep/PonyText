// File: PonyParser.cs 
// Copyright 2020 Zelong Ou. All Rights Reserved.
// Licensed under the GNU General Public License v3.0

using System.Collections.Generic;
using PonyText.Common.Parser;
using PonyText.Common.Parser.EventListener;
using PonyText.Common.Structure;
using PonyText.Common.Structure.Builder;
using PonyText.Parser.EventListener;
using PonyText.Parser.Tokens;

namespace PonyText.Parser {
    public class PonyParser : IErrorSource
    {
        private int[,] transitionTable = new int[,]
        {
            //L  $  @  {  [  ]  }  %  : ID  Q  LF NUM  (   )
            {10, 1,-1,-1,-1,-1,-1, 3,-1,-1,-1, 0,  -1, -1,-1},    //0
            {-1,-1,-1,-1, 2,-1,-1,-1,-1,-1,-1, 4,  -1, -1,-1},    //1
            {-1,-1, 6,-1,-1,-1,-1,-1,-1, 5,-1,-1,  -1, -1,-1},    //2
            {-1,-1,-1, 7,-1,10,-1, 0,-1, 3, 3, 4,   3,  2, 3},    //3   <ARGS>
            {-1, 0, 6,-1,-1,-1,-1,-1,-1, 5,-1, 4,  -1, -1,-1},    //4
            {-1,-1,-1, 3,-1,10, 3, 0, 3, 3, 3, 4,   3,  2, 3},    //5
            {-1,-1,-1,-1,-1,-1,-1,-1,-1, 3,-1,-1,  -1, -1,-1},    //6
            {-1,-1,-1,-1,-1,-1, 3,-1,-1, 8, 8, 7,  -1, -1,-1},    //7
            {-1,-1,-1,-1,-1,-1,-1,-1, 9,-1,-1,-1,  -1, -1,-1},    //8
            {-1,-1,-1,-1,-1,-1,-1,-1,-1, 7, 7, 9,   7, -1,-1},    //9
            {10, 1,-1,-1,-1,-1,-1, 3,-1,-1,-1, 0,  -1, -1,-1},    //10  <PARAGRAPH>
        };

        private Stack<PonyTokenType> context = new Stack<PonyTokenType>();
        private ITokenGenerator tokenGenerator;
        private StructureBuilder builder;

        public IErrorListener errorListener
        {
            get; set;
        }

        public PonyParser(ITokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
            tokenGenerator.onTokenGenerated += parseNext;
        }

        public PonyTextStructureBase Parse(string source)
        {
            builder = new StructureBuilder();
            builder.StartTextStructure(PonyToken.Empty);

            bool result = tokenGenerator.RunGenerator(source);
            if (context.Count != 0)
            {
                errorListener.OnParserErrorReported("Unexpected termination", currentState, string.Empty);
            }
            if (result)
            {
                return builder.BuildStructure();
            }
            return null;
        }

        int currentState = 0;
        private void parseNext(PonyToken token)
        {
            int colSelector = getTokenMapping(token);
            if (colSelector == -1)
            {
                tokenGenerator.HaltGenerator();
                errorListener.OnParserErrorReported("Unrecognized Token", currentState, token.ToString());
                return;
            }
            int nextState = transitionTable[currentState, colSelector];
            if (nextState == -1)
            {
                tokenGenerator.HaltGenerator();
                errorListener.OnParserErrorReported("Invalid Transition", currentState, token.ToString());
                return;
            }
            buildTextStruct(nextState, token);
            currentState = nextState;
        }

        string tokenAsKey;
        private void buildTextStruct(int nextState, PonyToken token)
        {
            if(currentState==10 && currentState == nextState) {
                builder.AddLiteral(token);
            }
            else if (currentState == 0 && nextState == 10)
            {
                builder.TryStartParagraph(token);
            }
            else if (nextState == 5)
            {
                builder.StartFormatableMarcoStruct(token);
            }
            else if(currentState == 1 && nextState == 2) {
                builder.TryStartParagraph(token.DeriveEmptyToken());
            }
            else if (currentState == 6 && nextState == 3)
            {
                builder.StartDirective(token);
            }
            else if (currentState == 1 && nextState == 4)
            {
                builder.StartDirectiveBlock(token.DeriveEmptyToken());
            }
            else if (nextState == 8)
            {
                tokenAsKey = token.Text.Trim('"');
            }
            else if (currentState == 9 && nextState == 7)
            {
                if (token.TokenType == PonyTokenType.ID)
                {
                    builder.AddMarcoMapping(tokenAsKey, token);
                }
                else if (token.TokenType == PonyTokenType.NUMBER)
                {
                    builder.AddNumMapping(tokenAsKey, token);
                }
                else if (token.TokenType == PonyTokenType.QUOTE)
                {
                    builder.AddLiteralMapping(tokenAsKey, token);
                }
            }
            else if ((currentState == 5 || currentState == 3) && nextState == 4)
            {
                builder.EndCurrentContext();
            }
            else if (currentState == 10 && nextState == 0)
            {
                builder.EndCurrentContext();
            }
            else if (currentState == 3)
            {
                if (token.TokenType == PonyTokenType.ID)
                {
                    builder.AddMarco(token);
                }
                else if (token.TokenType == PonyTokenType.NUMBER)
                {
                    builder.AddNumber(token);
                }
                else if (token.TokenType == PonyTokenType.LITERAL)
                {
                    builder.AddLiteral(token);
                }
                else if (token.TokenType == PonyTokenType.QUOTE)
                {
                    builder.AddLiteral(token);
                }
            }
        }


        //L  $  @  {  [  ]  }  %  : ID  Q  LF  NUM  (  )
        //0  1  2  3  4  5  6  7  8  9  10 11   12  13 14
        private int getTokenMapping(PonyToken token)
        {
            PonyTokenType tokenType = token.TokenType;
            PonyTokenType examedToken;
            switch (tokenType)
            {
                case PonyTokenType.LITERAL:
                    return 0;
                case PonyTokenType.LBRACE:
                    builder.StartMappingStruct(token);
                    context.Push(tokenType);
                    return 3;
                case PonyTokenType.RBRACE:
                    if (!context.TryPop(out examedToken) || examedToken != PonyTokenType.LBRACE) {
                        return -1;
                    }
                    builder.EndCurrentContext();
                    return 6;
                case PonyTokenType.LBRACKET:
                    context.Push(tokenType);
                    return 4;
                case PonyTokenType.RBRACKET:
                    if (!context.TryPop(out examedToken) || examedToken != PonyTokenType.LBRACKET) {
                        return -1;
                    }
                    if (!context.TryPop(out examedToken) || examedToken != PonyTokenType.DOLLAR) {
                        return -1;
                    }
                    builder.EndCurrentContext();
                    return 5;
                case PonyTokenType.LPAREN:
                    context.Push(tokenType);
                    return 13;
                case PonyTokenType.RPAREN:
                    if (!context.TryPop(out examedToken) || examedToken != PonyTokenType.LPAREN)
                    {
                        return -1;
                    }
                    builder.EndCurrentContext();
                    return 14;
                case PonyTokenType.AT:
                    return 2;
                case PonyTokenType.ID:
                    return 9;
                case PonyTokenType.CRLF:
                    if (currentState == 3 && currentState == 5)
                    {
                        if (!context.TryPeek(out examedToken) || examedToken != PonyTokenType.DOLLAR)
                        {
                            return -1;
                        }
                    }
                    return 11;
                case PonyTokenType.QUOTE:
                    return 10;
                case PonyTokenType.COLON:
                    return 8;
                case PonyTokenType.DOLLAR:
                    if (currentState == 4)
                    {
                        if (!context.TryPop(out examedToken) || examedToken != PonyTokenType.DOLLAR)
                            return -1;
                        builder.EndCurrentContext();
                        return 1;
                    }
                    context.Push(tokenType);
                    return 1;
                case PonyTokenType.PERCENTAGE:
                    if (currentState == 0 || currentState == 10)
                    {
                        if (!context.TryPop(out examedToken) || examedToken != PonyTokenType.PERCENTAGE)
                            return -1;
                        builder.EndCurrentContext();
                        if(currentState == 10) {
                            // if we transit from 10. then the prev end ctx is only
                            // end the paragraph struct. But the TextStruct ctx not ended.
                            // so we need end for one more time thus to ensure we leave the TextStruct completely.
                            builder.EndCurrentContext();
                        }
                        return 7;
                    }
                    builder.StartTextStructure(token);
                    context.Push(tokenType);
                    return 7;
                case PonyTokenType.NUMBER:
                    return 12;
                default:
                    return -1;
            }
        }
    }
}
