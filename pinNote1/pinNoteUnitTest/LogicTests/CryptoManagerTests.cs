using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretScrambler.CryptoTool;
using SecretScrambler.Logic;
using SecretScrambler.Models;

namespace SecretScramblerUnitTest.LogicTests
{
    [TestClass]
    public class CryptoManagerTests
    {
        public TestContext TestContext { get; set; }

        [DataTestMethod]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", false, false, false)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", false, false, true)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", false, true, false)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", false, true, true)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", true, false, false)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", true, false, true)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", true, true, false)]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=", true, true, true)]


        public void CombinedCryptoToolTest(string message, string password, bool aes, bool tdes, bool hum)
        {
            TestContext.WriteLine($"Params| message = {message}, password = {password}");

            var sm = new SecurityModel() {
                SelectedAES = aes,
                SelectedTripleDES = tdes,
                SelectedHumbleCrypt = hum
            };

            var cryptoTools = CryptoManager.BuildCryptoToolsFromSecurityModel(sm);

            var originalMessage = message;

            var encrypted = CryptoManager.RunSelectedEncryption(cryptoTools, message, password);
            var decrypted = CryptoManager.RunSelectedDecryption(cryptoTools, encrypted, password);

            TestContext.WriteLine($"Encrypted message = {encrypted}");
            TestContext.WriteLine($"Has at least one CryptoToolSelect = {aes | tdes | hum}");

            if (aes | tdes | hum)
                Assert.AreNotEqual(encrypted, decrypted);
            
            Assert.AreEqual(originalMessage, decrypted);
        }
    }
}
