using CommonUtilitiesCSharp.Core;
using System.Runtime.CompilerServices;

namespace CommonUtilitiesCSharp.UnitTests.Core
{
    public class ObservableObjectTests
    {
        private class MockObject : ObservableObject
        {
            public MockObject(string value)
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
            var mockObject = new MockObject("test");

            Assert.That(mockObject.HasPropertyChangedBeenCalled, Is.False);

            mockObject.Value = "test2";

            Assert.That(mockObject.HasPropertyChangedBeenCalled, Is.True);
        }

        [Test]
        public void OnPropertyChanged_RaisesPropertyChangedEvent()
        {
            var mockObject = new MockObject("test");
            var flag = false;
            var propertyName = "";

            mockObject.PropertyChanged += (sender, args) =>
            {
                flag = true;
                propertyName = args.PropertyName;
            };

            Assert.Multiple(() =>
            {
                Assert.That(flag, Is.False);
                Assert.That(propertyName, Is.EqualTo(""));
            });

            mockObject.Value = "test2";

            Assert.Multiple(() =>
            {
                Assert.That(flag, Is.True);
                Assert.That(propertyName, Is.EqualTo(nameof(MockObject.Value)));
            });
        }
    }
}
