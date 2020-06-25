using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecretScrambler.CryptoTool;

namespace SecretScramblerUnitTest.CryptoToolTests
{
    [TestClass]
    public class HumbleCryptoToolTests
    {
        public TestContext TestContext { get; set; }

        [DataTestMethod]
        [DataRow("Hello World","Password1!")]
        [DataRow("What is the meaning of life.  I don't know?  and even more random text!!!!!", "Password1!")]
        [DataRow("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        , "Password1!")]
        [DataRow(
        "A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet I feel that I never was a greater artist than now.  When, while the lovely valley teems with vapour around me, and the meridian sun strikes the upper surface of the impenetrable foliage of my trees, and but a few stray gleams steal into the inner sanctuary, I throw myself down among the tall grass by the trickling stream; and, as I lie close to the earth, a thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath of that universal love which bears and sustains us, as it floats around us in an eternity of bliss; and then, my friend, when darkness overspreads my eyes, and heaven and earth seem to dwell in my soul and absorb its power, like the form of a beloved mistress, then I often think with longing, Oh, would I could describe these conceptions, could impress upon paper all that is living so full and warm within me, that it might be the mirror of my soul, as my soul is the mirror of the infinite God! O my friend -- but it is too much for my strength -- I sink under the weight of the splendour of these visions! A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart.I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil existence, that I neglect my talents.I should be incapable of drawing a single stroke at the present moment; and"
            , "Password1!")]
        [DataRow("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 `-=~!@#$%^&*()_+[];';,./{}:\"<>?|\\", "Password1!")]
        public void EncryptionAndDecryptionMessageTest(string message, string password)
        {
            TestContext.WriteLine($"Params| message = {message}, password = {password}");

            var hct = new HumbleCryptoTool();

            var originalMessage = message;

            var encrypted = hct.EncryptRun(message, password);
            var decrypted = hct.DecryptRun(encrypted, password);

            TestContext.WriteLine($"Encrypted message = {encrypted}");

            Assert.AreNotEqual(encrypted, decrypted);
            Assert.AreEqual(originalMessage, decrypted);
        }

        [DataTestMethod]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "1234")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "2345")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "12345")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "abcd")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "bcde")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "abcde")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "abcd1234")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "abcde12345")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "1H2e3L4l5O6w7O8r9L0d")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "a!@#$%^&*(),./;'z")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "Password1!")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "v0vWHyAmVjI=")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "DR4vowDLQf7ukA+osSSnmA==")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "yUwOji72Aos=Gf6qh4/EkR4wIENPi4176A==")]
        [DataRow("The quick, brown fox jumps over a lazy dog. DJs flock by when MTV ax quiz prog. Junk MTV quiz graced by fox whelps. Bawds jog, flick quartz, vex nymphs.", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 `-=~!@#$%^&*()_+[];';,./{}:\"<>?|\\")]
        public void EncryptionAndDecryptionPasswordTest(string message, string password)
        {
            TestContext.WriteLine($"Params| message = {message}, password = {password}");

            var hct = new HumbleCryptoTool();

            var originalMessage = message;

            var encrypted = hct.EncryptRun(message, password);
            var decrypted = hct.DecryptRun(encrypted, password);

            TestContext.WriteLine($"Encrypted message = {encrypted}");

            Assert.AreNotEqual(encrypted, decrypted);
            Assert.AreEqual(originalMessage, decrypted);
        }
    }
}
