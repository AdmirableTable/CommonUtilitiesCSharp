using CommonUtilitiesCSharp.Core;
using System.Runtime.CompilerServices;

namespace CommonUtilitiesCSharp.UnitTests.Core
{
    public class ObservableObjectTests
    {
        private class MockObservableObject : ObservableObject
        {
            public MockObservableObject(string value)
            {
                _value = value;
            }

            private string _value;
            public string Value
            {
                get => _value;
                set
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }

            public string IndirectValue
            {
                get => _value;
                set
                {
                    _value = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Value));
                }
            }

            public bool HasPropertyChangedBeenCalled { get; private set; }

            protected override void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                HasPropertyChangedBeenCalled = true;
                base.OnPropertyChanged(propertyName);
            }
        }

        [Test]
        public void OnPropertyChanged_CanBeOverriden()
        {
            var mockObject = new MockObservableObject("test");

            Assert.That(mockObject.HasPropertyChangedBeenCalled, Is.False);

            mockObject.Value = "test2";
            
            Assert.Multiple(() =>
            {
                Assert.That(mockObject.Value, Is.EqualTo("test2"));
                Assert.That(mockObject.HasPropertyChangedBeenCalled, Is.True);
            });
        }

        [Test]
        public void OnPropertyChanged_RaisesPropertyChangedEvent_WhenInvokedWithoutName()
        {
            var mockObject = new MockObservableObject("test");
            var propertiesChanged = new List<string?>();

            mockObject.PropertyChanged += (sender, args) =>
            {
                propertiesChanged.Add(args.PropertyName);
            };

            Assert.That(propertiesChanged, Is.Empty);

            mockObject.Value = "test2";

            Assert.Multiple(() =>
            {
                Assert.That(propertiesChanged, Has.Count.EqualTo(1));
                Assert.That(propertiesChanged[0], Is.EqualTo(nameof(MockObservableObject.Value)));
            });
        }

        [Test]
        public void OnPropertyChanged_RaisesPropertyChangedEvent_WhenInvokedWithName()
        {
            var mockObject = new MockObservableObject("test");
            var propertiesChanged = new List<string?>();

            mockObject.PropertyChanged += (sender, args) =>
            {
                propertiesChanged.Add(args.PropertyName);
            };

            Assert.That(propertiesChanged, Is.Empty);

            mockObject.IndirectValue = "test2";

            Assert.Multiple(() =>
            {
                Assert.That(mockObject.IndirectValue, Is.EqualTo("test2"));
                Assert.That(propertiesChanged, Has.Count.EqualTo(2));
                Assert.That(propertiesChanged[0], Is.EqualTo(nameof(MockObservableObject.IndirectValue)));
                Assert.That(propertiesChanged[1], Is.EqualTo(nameof(MockObservableObject.Value)));
            });
        }
    }
}
