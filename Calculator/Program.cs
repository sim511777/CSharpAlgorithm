using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    class Calculator {
        public static decimal Evalute(string[] infixTokens) {
            // 중위표기를 후위표기로 변환
            var postifxTokens = ConvertToPostfix(infixTokens);

            // 후위표기 식 계산
            string[] operators = { "+", "-", "*", "/" };
            var stk = new Stack<string>();

            foreach (var tok in postifxTokens) {
                if (operators.Contains(tok)) {
                    var n2 = decimal.Parse(stk.Pop());
                    var n1 = decimal.Parse(stk.Pop());
                    var res = Calc(tok, n1, n2);
                    stk.Push(res.ToString());
                } else {
                    stk.Push(tok);
                }
            }

            // 스택에서 마지막 결과 Pop
            string result = stk.Pop();
            return decimal.Parse(result);
        }

        // 중위표기를 후위표기로 변환
        private static string[] ConvertToPostfix(string[] infix) {
            var postfix = new List<string>();
            var stk = new Stack<string>();

            foreach (var tok in infix) {
                if (tok == "(") {
                    stk.Push(tok);
                } else if (tok == ")") {
                    while (stk.Count > 0) {
                        var t = stk.Pop();
                        if (t == "(") break;

                        postfix.Add(t);
                    }
                } else if (tok == "+" || tok == "-") {
                    while (stk.Count > 0 && stk.Peek() != "(") {
                        postfix.Add(stk.Pop());
                    }
                    stk.Push(tok);
                } else if (tok == "*" || tok == "/") {
                    while (stk.Count > 0 && (stk.Peek() == "*" || stk.Peek() == "/")) {
                        postfix.Add(stk.Pop());
                    }
                    stk.Push(tok);
                } else {
                    postfix.Add(tok);
                }
            }

            //스택에 남은 연산자 모두 추가
            while (stk.Count > 0) {
                postfix.Add(stk.Pop());
            }

            return postfix.ToArray();
        }

        private static decimal Calc(string op, decimal n1, decimal n2) {
            decimal result = 0;

            if (op == "+") {
                result = n1 + n2;
            } else if (op == "-") {
                result = n1 - n2;
            } else if (op == "*") {
                result = n1 * n2;
            } else if (op == "/") {
                result = n1 / n2;
            } else {
                throw new InvalidOperationException();
            }

            return result;
        }
    }

    class Program {
        static void Main(string[] args) {
            // 테스트 수식
            string[] tokens = "2 * 3.4 + ( 15 - 2 ) / 2".Split(' ');

            var result = Calculator.Evalute(tokens);
            Console.WriteLine(result);
        }
    }
}
