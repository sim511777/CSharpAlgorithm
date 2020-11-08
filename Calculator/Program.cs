using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    class Calculator {
        public static decimal Evalute(string expression) {
            string[] infixTokens = Tokenize(expression);
            var postfixTokens = ConvertToPostfix(infixTokens);
            var result = CalcPostfix(postfixTokens);
            return result;
        }

        // 토큰화
        public static string[] Tokenize(string expression) {
            return expression.Split(' ');
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

        // 후위표기식 계산
        public static decimal CalcPostfix(string[] postfixTokens) {
            string[] operators = { "+", "-", "*", "/" };
            var stk = new Stack<string>();

            foreach (var tok in postfixTokens) {
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

        // 연산자와 피연산자로 값 계산
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

/*
infix             => postfix
1 * 2 + 3 * 4     => 1 2 * 3 4 * +
1 + 2 * 3 + 4     => 1 2 3 * + 4 +
1 + 2 + 3         => 1 2 + 3 +
1 + 2 * 3         => 1 2 3 * +
1 * 2 + 3         => 1 2 * 3 +
( 1 + 2 ) * 3     => 1 2 + 3 *
1 * ( 2 + 3 )     => 1 2 3 + *
1 * ( 2 + 3 * 4 ) => 1 2 3 4 * + *

## infix를 postfix로 변환
infix를 하나씩 꺼내서
  피연산자 이면 posfix에 추가
  연산자이면 스택에서 나보다 높은 연산자가 나오기 전까지 스택에서 팝해서 postfix에 추가 하고 나느 스택에 추가
  열괄호이면 스택에 추가
  닫괄호이면 열괄호가 나올때 까지 스택에서 팝하여 postfix에 추가
스택에 남은 연산자들을 모드 postfix에 추가

## postfix를 계산
postfix를 하나씩 꺼내서
  피연산자이면 스택에 추가
  연산자이면 스택에서 피연산자 두개를 꺼내서 계산하고 그 결과를 스택에 추가
스택에 남은 마지막 값이 최종 결과 값
*/

    class Program {
        static void Main(string[] args) {
            // 테스트 수식
            string expression = "2 * 3.4 + ( 15 - 2 ) / 2";
            var result = Calculator.Evalute(expression);
            Console.WriteLine(result);
        }
    }
}
