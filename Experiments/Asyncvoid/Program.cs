using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Asyncvoid {
    class Program {
        
        abstract class A<T>{
            public A(int len) {
                this.Build(len);
            }
            public List<T> list;
            protected abstract void Build(int len);
            public virtual void Use(T i) {
                this.list.Add(i);
            }
        }
        class B : A<int> {
            public B(int len):base(len) {

            }
            protected override void Build(int len) {
                this.list = new List<int>(len);
            }
        }
        static async Task Main(string[] args) {

            B a = new B(3);
            a.Use(33);
           
        }

      
    }
}
