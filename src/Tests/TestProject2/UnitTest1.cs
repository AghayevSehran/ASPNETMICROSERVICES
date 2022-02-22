using NUnit.Framework;
using Test01;

namespace TestProject2
{
    public class Tests
    {
        private double epsilon = 1e-6;
        [SetUp]
        [Test]
        public void AccountCannotHaveNegativeOverdraftLimit()
        {
            Account account = new Account(-20);

            Assert.AreEqual(0, account.OverdraftLimit, epsilon);
        }

        [Test]
        public void PositiveDepositAndWithdraw() //Testcase1
        {
            Account account = new Account(-20);

            Assert.AreEqual(false, account.Deposit(-20));
            Assert.AreEqual(false, account.Withdraw(-20));
        }

        [Test]
        public void OverdraftLimit() //Testcase2
        {
            Account account = new Account(100);

            Assert.AreEqual(false, account.Withdraw(120));
        }

        [Test]
        public void CorrectResult() //Testcase4
        {
            Account account = new Account(100);

            Assert.AreEqual(true, account.Deposit(50));
            Assert.AreEqual(true, account.Withdraw(20));
        }
    }
}