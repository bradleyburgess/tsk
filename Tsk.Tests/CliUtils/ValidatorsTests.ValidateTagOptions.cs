using Tsk.CLI.Utils;
using Tsk.CLI.Application.Commands;

namespace Tsk.Tests.CliUtils
{
    public class ValidatorsTests_ValidateTagOptions
    {
        [Fact]
        public void ShouldNotError_With_UpdateTagsOption()
        {
            var settings = new UpdateCommand.Settings();
            settings.UpdateTags = "jogging";
            try
            {
                CommandValidators.ValidateTagOptions(settings);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no error, but got: " + ex.Message);
            }
        }

        [Fact]
        public void ShouldNotError_With_AddTagsOption()
        {
            var settings = new UpdateCommand.Settings();
            settings.AddTags = "jogging";
            try
            {
                CommandValidators.ValidateTagOptions(settings);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no error, but got: " + ex.Message);
            }
        }

        [Fact]
        public void ShouldNotError_With_NoTagOptions()
        {
            var settings = new UpdateCommand.Settings();
            try
            {
                CommandValidators.ValidateTagOptions(settings);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no error, but got: " + ex.Message);
            }
        }

        [Fact]
        public void ShouldError_With_TwoTagOptions()
        {
            var settings = new UpdateCommand.Settings();
            settings.AddTags = "add,tags";
            settings.RemoveTags = "remove";
            Assert.Throws<ArgumentException>(() => CommandValidators.ValidateTagOptions(settings));
        }
    }
}