using FluentAssertions;
using YiChing.Configuration;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace TestHexagram
{
    [TestFixture]
    public class UrlManagementTests
    {
        private Settings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = new Settings();
        }

        [TearDown]
        public void Teardown()
        {
            _settings.Dispose();
        }

        [Test]
        public void DefaultUrls_ShouldContainExpectedValues()
        {
            // Arrange & Act
            var defaultUrls = JsonSerializer.Deserialize<string[]>(DefaultTexts.DEFAULT_URLS);

            // Assert
            defaultUrls.Should().NotBeNull();
            defaultUrls.Should().ContainInOrder(
                "https://chat.deepseek.com",
                "https://claude.ai/new",
                "https://chatgpt.com/"
            );
        }

        [Test]
        public void AddUrl_ShouldAddValidUrl()
        {
            // Arrange
            var newUrl = "https://example.com";

            // Act
            _settings.SavedUrls.Add(newUrl);

            // Assert
            _settings.SavedUrls.Should().Contain(newUrl);
        }

        [Test]
        public void AddUrl_ShouldNotAddInvalidUrl()
        {
            // Arrange
            var invalidUrl = "not-a-valid-url";

            // Act
            _settings.SavedUrls.Add(invalidUrl);

            // Assert
            _settings.SavedUrls.Should().NotContain(invalidUrl);
        }

        [Test]
        public void RemoveUrl_ShouldRemoveSelectedUrl()
        {
            // Arrange
            var urlToRemove = _settings.SavedUrls.First();
            _settings.SelectedUrl = urlToRemove;

            // Act
            _settings.SavedUrls.Remove(urlToRemove);

            // Assert
            _settings.SavedUrls.Should().NotContain(urlToRemove);
            _settings.SelectedUrl.Should().BeNullOrEmpty();
        }

        [Test]
        public void SaveValues_ShouldPersistUrls()
        {
            // Arrange
            var testUrl = "https://test.com";
            _settings.SavedUrls.Add(testUrl);

            // Act
            _settings.SaveValues();
            var savedUrls = Preferences.Default.Get(nameof(Settings.SavedUrls), string.Empty);

            // Assert
            savedUrls.Should().NotBeNullOrEmpty();
            savedUrls.Should().Contain(testUrl);
        }

        [Test]
        public void LoadValues_ShouldRestoreUrls()
        {
            // Arrange
            var testUrls = new[] { "https://test1.com", "https://test2.com" };
            var json = JsonSerializer.Serialize(testUrls);
            Preferences.Default.Set(nameof(Settings.SavedUrls), json);

            // Act
            _settings.LoadValues(null);

            // Assert
            _settings.SavedUrls.Should().BeEquivalentTo(testUrls);
        }

        [Test]
        public void Reset_ShouldRestoreDefaultUrls()
        {
            // Arrange
            _settings.SavedUrls.Add("https://custom.com");

            // Act
            _settings = new Settings();

            // Assert
            _settings.SavedUrls.Should().BeEquivalentTo(
                JsonSerializer.Deserialize<string[]>(DefaultTexts.DEFAULT_URLS)
            );
        }

        [Test]
        public void SelectedUrl_ShouldUpdateWhenChanged()
        {
            // Arrange
            var testUrl = "https://test.com";
            _settings.SavedUrls.Add(testUrl);

            // Act
            _settings.SelectedUrl = testUrl;

            // Assert
            _settings.SelectedUrl.Should().Be(testUrl);
        }
    }
}