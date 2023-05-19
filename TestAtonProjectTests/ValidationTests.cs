using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestAtonProjectTests
{
    public class ValidationTests
    {
        // 5 login tests
        [Fact]
        public void LatinOrNumbersValidation_Login1_ReturnsTrue()
        {
            Assert.True(User.LatinOrNumbersValidation("Login90"));
        }

        [Fact]
        public void LatinOrNumbersValidation_Login2_ReturnsTrue()
        {
            Assert.True(User.LatinOrNumbersValidation("12321afafadjfhdajk231"));
        }

        [Fact]
        public void LatinOrNumbersValidation_Login3_ReturnsFalse()
        {
            Assert.False(User.LatinOrNumbersValidation("Cool Login"));
        }

        [Fact]
        public void LatinOrNumbersValidation_Login4_ReturnsFalse()
        {
            Assert.False(User.LatinOrNumbersValidation("@login000."));
        }

        [Fact]
        public void LatinOrNumbersValidation_Login5_ReturnsFalse()
        {
            Assert.False(User.LatinOrNumbersValidation(""));
        }

        //5 password tests
        [Fact]
        public void LatinOrNumbersValidation_Password1_ReturnsTrue()
        {
            Assert.True(User.LatinOrNumbersValidation("9password1"));
        }

        [Fact]
        public void LatinOrNumbersValidation_Password2_ReturnsTrue()
        {
            Assert.True(User.LatinOrNumbersValidation("1234567890"));
        }

        [Fact]
        public void LatinOrNumbersValidation_Password3_ReturnFalse()
        {
            Assert.False(User.LatinOrNumbersValidation("9password1v "));
        }

        [Fact]
        public void LatinOrNumbersValidation_Password4_ReturnsFalse()
        {
            Assert.False(User.LatinOrNumbersValidation(""));
        }

        [Fact]
        public void LatinOrNumbersValidation_Password_ReturnsFalse()
        {
            Assert.False(User.LatinOrNumbersValidation("Паролль"));
        }

        //5 name tests
        [Fact]
        public void LatinOrCyrillicValidation_Name1_ReturnsTrue()
        {
            Assert.True(User.LatinOrCyrillicValidation("Евграф"));
        }

        [Fact]
        public void LatinOrCyrillicValidation_Name2_ReturnsTrue()
        {
            Assert.True(User.LatinOrCyrillicValidation("Иваноzavr"));
        }

        [Fact]
        public void LatinOrCyrillicValidation_Name3_ReturnsFalse()
        {
            Assert.False(User.LatinOrCyrillicValidation("Евграф 1"));
        }

        [Fact]
        public void LatinOrCyrillicValidation_Name4_ReturnsFalse()
        {
            Assert.False(User.LatinOrCyrillicValidation("Geek1234"));
        }

        [Fact]
        public void LatinOrCyrillicValidation_Name5_ReturnsFalse()
        {
            Assert.False(User.LatinOrCyrillicValidation("MegaProger2022"));
        }
    }
}
