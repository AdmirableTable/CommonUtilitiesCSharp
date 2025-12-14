using CommonUtilitiesCSharp.Core;

namespace CommonUtilitiesCSharp.UnitTests.Core
{
    public abstract class CommandTestBase<TCommandParameter>
    {
        protected abstract Command CreateCommand(Action<TCommandParameter?> execute);

        protected abstract Command CreateCommand(Func<TCommandParameter?, Task> execute);

        protected abstract Command CreateCommand(Action<TCommandParameter?> execute, Predicate<TCommandParameter?> canExecute);

        protected abstract Command CreateCommand(Func<TCommandParameter?, Task> execute, Predicate<TCommandParameter?> canExecute);

        #region Constructor
        [Test]
        public void Constructor_Throws_ForNullExecute()
        {
            Assert.Throws<ArgumentNullException>(() => CreateCommand((Action<TCommandParameter?>)null!));
            Assert.Throws<ArgumentNullException>(() => CreateCommand((Func<TCommandParameter?, Task>)null!));
        }

        [Test]
        public void Constructor_Throws_ForNullCanExecute()
        {
            static void execute(TCommandParameter? param) { throw new NotSupportedException(); }
            static Task asyncExecute(TCommandParameter? param) { throw new NotSupportedException(); }

            // Check the execute functions would behave as expected if the test failed
            Assert.Throws<NotSupportedException>(() => execute(default));
            Assert.ThrowsAsync<NotSupportedException>(() => asyncExecute(default));

            // Actual test
            Assert.Throws<ArgumentNullException>(() => CreateCommand(execute, null!));
            Assert.Throws<ArgumentNullException>(() => CreateCommand(asyncExecute, null!));
        }

        [Test]
        public void Constructor_Throws_ForNullExecuteWithPredicate()
        {
            static bool canExecute(TCommandParameter? param) { throw new NotSupportedException(); }

            Assert.Throws<NotSupportedException>(() => canExecute(default));
            Assert.Throws<ArgumentNullException>(() => CreateCommand(null!, canExecute));
        }
        #endregion Constructor

        #region Execute
        [Test]
        public abstract Task Execute_IsInvoked_WhenThereIsNoPredicate();

        [Test]
        public abstract Task Execute_IsInvokedAsync_WhenThereIsNoPredicate();

        [Test]
        public abstract Task Execute_IsNotInvoked_WhenPredicateIsFalse();

        [Test]
        public abstract Task Execute_IsNotInvokedAsync_WhenPredicateIsFalse();

        [Test]
        public abstract Task Execute_IsInvoked_WhenPredicateIsTrue();

        [Test]
        public abstract Task Execute_IsInvokedAsync_WhenPredicateIsTrue();

        [Test]
        public abstract Task Execute_IsInvokedWithParameter_WhenParameterIsPassed();

        [Test]
        public abstract Task Execute_IsInvokedAsyncWithParameter_WhenParameterIsPassed();

        [Test]
        public abstract Task Execute_IsInvokedWithParameter_WhenParameterIsPassedAndPredicateIsTrue();

        [Test]
        public abstract Task Execute_IsInvokedAsyncWithParameter_WhenParameterIsPassedAndPredicateIsTrue();
        #endregion Execute

        #region CanExecute
        [Test]
        public abstract void CanExecute_ReturnsTrue_WhenThereIsNoPredicate();

        [Test]
        public abstract void CanExecute_ReturnsFalse_WhenPredicateIsFalse();

        [Test]
        public abstract void CanExecute_ReturnsTrue_WhenPredicateIsTrue();

        [Test]
        public abstract void CanExecute_ReturnsFalse_WhenParameterIsPassedAndPredicateIsFalse();

        [Test]
        public abstract void CanExecute_ReturnsTrue_WhenParameterIsPassedAndPredicateIsTrue();
        #endregion CanExecute
    }

    public class CommandTests : CommandTestBase<object>
    {
        protected override Command CreateCommand(Action<object?> execute) => new(execute);

        protected override Command CreateCommand(Func<object?, Task> execute) => new(execute);

        protected override Command CreateCommand(Action<object?> execute, Predicate<object?> canExecute) => new(execute, canExecute);

        protected override Command CreateCommand(Func<object?, Task> execute, Predicate<object?> canExecute) => new(execute, canExecute);

        #region Execute
        [Test]
        public async override Task Execute_IsInvoked_WhenThereIsNoPredicate()
        {
            var flag = false;
            var command = CreateCommand(parameter => flag = true);

            Assert.That(flag, Is.False);

            await command.Execute();

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsync_WhenThereIsNoPredicate()
        {
            var task = new TaskCompletionSource();

            var command = CreateCommand(async parameter =>
            {
                task.SetResult();
                await Task.CompletedTask;
            });

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute();

            Assert.That(task.Task.IsCompleted, Is.True);
        }

        [Test]
        public async override Task Execute_IsNotInvoked_WhenPredicateIsFalse()
        {
            var flag = false;
            var condition = false;
            var command = CreateCommand(parameter => flag = true, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(flag, Is.False);
                Assert.That(condition, Is.False);
            });

            await command.Execute();

            Assert.That(flag, Is.False);
        }

        [Test]
        public async override Task Execute_IsNotInvokedAsync_WhenPredicateIsFalse()
        {
            var task = new TaskCompletionSource();
            var condition = false;

            var command = CreateCommand(async parameter =>
            {
                task.SetResult();
                await Task.CompletedTask;
            }, parameter => condition);

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute();

            Assert.That(task.Task.IsCompleted, Is.False);
        }

        [Test]
        public async override Task Execute_IsInvoked_WhenPredicateIsTrue()
        {
            var flag = false;
            var condition = false;
            var command = CreateCommand(parameter => flag = true, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(flag, Is.False);
                Assert.That(condition, Is.False);
            });

            condition = true;
            await command.Execute();

            Assert.That(flag, Is.True);
        }
        public async override Task Execute_IsInvokedAsync_WhenPredicateIsTrue()
        {
            var task = new TaskCompletionSource();
            var condition = false;
            var command = CreateCommand(async parameter =>
            {
                task.SetResult();
                await Task.CompletedTask;
            }, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.False);
                Assert.That(condition, Is.False);
            });

            condition = true;
            await command.Execute();

            Assert.That(task.Task.IsCompleted, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedWithParameter_WhenParameterIsPassed()
        {
            var flag = false;
            var command = CreateCommand(parameter => flag = (bool)parameter!);

            Assert.That(flag, Is.False);

            await command.Execute(true);

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsyncWithParameter_WhenParameterIsPassed()
        {
            var task = new TaskCompletionSource<bool>();
            var command = CreateCommand(async parameter =>
            {
                task.SetResult((bool)parameter!);
                await Task.CompletedTask;
            });

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute(true);
            
            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.True);
                Assert.That(task.Task.Result, Is.True);
            });
        }

        [Test]
        public async override Task Execute_IsInvokedWithParameter_WhenParameterIsPassedAndPredicateIsTrue()
        {
            var flag = false;
            var command = CreateCommand(parameter =>
            {
                var parameters = (bool[])parameter!;
                flag = parameters[0];
            }, parameter =>
            {
                var parameters = (bool[])parameter!;
                return !parameters[1];
            });

            Assert.That(flag, Is.False);

            await command.Execute(new[] { true, false });

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsyncWithParameter_WhenParameterIsPassedAndPredicateIsTrue()
        {
            var task = new TaskCompletionSource<bool>();
            var command = CreateCommand(async parameter =>
            {
                var parameters = (bool[])parameter!;
                task.SetResult(parameters[0]);
                await Task.CompletedTask;
            }, parameter =>
            {
                var parameters = (bool[])parameter!;
                return !parameters[1];
            });

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute(new[] { true, false });

            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.True);
                Assert.That(task.Task.Result, Is.True);
            });
        }
        #endregion Execute

        #region CanExecute
        [Test]
        public override void CanExecute_ReturnsTrue_WhenThereIsNoPredicate()
        {
            var command = CreateCommand(parameter => { });

            Assert.That(command.CanExecute(), Is.True);
        }

        [Test]
        public override void CanExecute_ReturnsFalse_WhenPredicateIsFalse()
        {
            var condition = false;
            var command = CreateCommand(parameter => { }, parameter => condition);

            Assert.That(command.CanExecute(), Is.False);
        }

        [Test]
        public override void CanExecute_ReturnsTrue_WhenPredicateIsTrue()
        {
            var condition = false;
            var command = CreateCommand(parameter => { }, parameter => condition);

            Assert.That(condition, Is.False);

            condition = true;

            Assert.That(command.CanExecute(), Is.True);
        }

        [Test]
        public override void CanExecute_ReturnsFalse_WhenParameterIsPassedAndPredicateIsFalse()
        {
            var command = CreateCommand(parameter => { }, parameter => (bool)parameter!);

            Assert.That(command.CanExecute(false), Is.False);
        }

        [Test]
        public override void CanExecute_ReturnsTrue_WhenParameterIsPassedAndPredicateIsTrue()
        {
            var command = CreateCommand(parameter => { }, parameter => (bool)parameter!);

            Assert.That(command.CanExecute(true), Is.True);
        }
        #endregion CanExecute

        #region ConvertParameter
        [Test]
        public void ConvertParameter_ReturnsParameter_WhenParameterIsOfTypeT()
        {
            object parameter = "test";

            var result = Command.ConvertParameter<string>(parameter);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(parameter));
                Assert.That(result, Is.TypeOf<string>());
            });
        }

        [Test]
        public void ConvertParameter_Throws_WhenParameterIsNotOfTypeT()
        {
            object parameter = 1;

            Assert.Throws<ArgumentException>(() => Command.ConvertParameter<string>(parameter));
        }

        [Test]
        public void ConvertParameter_Throws_WhenParameterIsNull()
        {
            object parameter = null!;

            Assert.Throws<ArgumentNullException>(() => Command.ConvertParameter<string>(parameter));
        }
        #endregion ConvertParameter
    }

    public class CommandTTests : CommandTestBase<string>
    {
        protected override Command<string> CreateCommand(Action<string?> execute) => new(execute);

        protected override Command<string> CreateCommand(Func<string?, Task> execute) => new(execute);

        protected override Command<string> CreateCommand(Action<string?> execute, Predicate<string?> canExecute) => new(execute, canExecute);

        protected override Command<string> CreateCommand(Func<string?, Task> execute, Predicate<string?> canExecute) => new(execute, canExecute);

        #region Execute
        [Test]
        public async override Task Execute_IsInvoked_WhenThereIsNoPredicate()
        {
            var flag = false;
            var command = CreateCommand(parameter => flag = true);

            Assert.That(flag, Is.False);

            await command.Execute("test");

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsync_WhenThereIsNoPredicate()
        {
            var task = new TaskCompletionSource();
            var command = CreateCommand(async parameter =>
            {
                task.SetResult();
                await Task.CompletedTask;
            });

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute("test");

            Assert.That(task.Task.IsCompleted, Is.True);
        }

        [Test]
        public async override Task Execute_IsNotInvoked_WhenPredicateIsFalse()
        {
            var flag = false;
            var condition = false;
            var command = CreateCommand(parameter => flag = true, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(flag, Is.False);
                Assert.That(condition, Is.False);
            });

            await command.Execute("test");

            Assert.That(flag, Is.False);
        }

        [Test]
        public async override Task Execute_IsNotInvokedAsync_WhenPredicateIsFalse()
        {
            var task = new TaskCompletionSource();
            var condition = false;
            var command = CreateCommand(async parameter =>
            {
                task.SetResult();
                await Task.CompletedTask;
            }, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.False);
                Assert.That(condition, Is.False);
            });

            await command.Execute("test");

            Assert.That(task.Task.IsCompleted, Is.False);
        }

        [Test]
        public async override Task Execute_IsInvoked_WhenPredicateIsTrue()
        {
            var flag = false;
            var condition = false;
            var command = CreateCommand(parameter => flag = true, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(flag, Is.False);
                Assert.That(condition, Is.False);
            });

            condition = true;
            await command.Execute("test");

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsync_WhenPredicateIsTrue()
        {
            var task = new TaskCompletionSource();
            var condition = false;
            var command = CreateCommand(async parameter =>
            {
                task.SetResult();
                await Task.CompletedTask;
            }, parameter => condition);

            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.False);
                Assert.That(condition, Is.False);
            });

            condition = true;
            await command.Execute("test");

            Assert.That(task.Task.IsCompleted, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedWithParameter_WhenParameterIsPassed()
        {
            var flag = false;
            var command = CreateCommand(parameter => flag = parameter == "success");

            Assert.That(flag, Is.False);

            await command.Execute("success");

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsyncWithParameter_WhenParameterIsPassed()
        {
            var task = new TaskCompletionSource<bool>();
            var command = CreateCommand(async parameter =>
            {
                task.SetResult(parameter == "success");
                await Task.CompletedTask;
            });

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute("success");
            
            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.True);
                Assert.That(task.Task.Result, Is.True);
            });
        }

        [Test]
        public async override Task Execute_IsInvokedWithParameter_WhenParameterIsPassedAndPredicateIsTrue()
        {
            var flag = false;
            var command = CreateCommand(
                parameter => flag = parameter == "success",
                parameter => parameter != "fail");

            Assert.That(flag, Is.False);

            await command.Execute("success");

            Assert.That(flag, Is.True);
        }

        [Test]
        public async override Task Execute_IsInvokedAsyncWithParameter_WhenParameterIsPassedAndPredicateIsTrue()
        {
            var task = new TaskCompletionSource<bool>();
            var command = CreateCommand(async parameter =>
            {
                task.SetResult(parameter == "success");
                await Task.CompletedTask;
            },
            parameter => parameter != "fail");

            Assert.That(task.Task.IsCompleted, Is.False);

            await command.Execute("success");

            Assert.Multiple(() =>
            {
                Assert.That(task.Task.IsCompleted, Is.True);
                Assert.That(task.Task.Result, Is.True);
            });
        }
        #endregion Execute

        #region CanExecute
        [Test]
        public override void CanExecute_ReturnsTrue_WhenThereIsNoPredicate()
        {
            var command = CreateCommand(parameter => { });

            Assert.That(command.CanExecute("test"), Is.True);
        }

        [Test]
        public override void CanExecute_ReturnsFalse_WhenPredicateIsFalse()
        {
            var condition = false;
            var command = CreateCommand(parameter => { }, parameter => condition);

            Assert.That(command.CanExecute("test"), Is.False);
        }

        [Test]
        public override void CanExecute_ReturnsTrue_WhenPredicateIsTrue()
        {
            var condition = false;
            var command = CreateCommand(parameter => { }, parameter => condition);

            Assert.That(condition, Is.False);

            condition = true;

            Assert.That(command.CanExecute("test"), Is.True);
        }

        [Test]
        public override void CanExecute_ReturnsFalse_WhenParameterIsPassedAndPredicateIsFalse()
        {
            var command = CreateCommand(parameter => { }, parameter => parameter == "success");

            Assert.That(command.CanExecute("fail"), Is.False);
        }

        [Test]
        public override void CanExecute_ReturnsTrue_WhenParameterIsPassedAndPredicateIsTrue()
        {
            var command = CreateCommand(parameter => { }, parameter => parameter == "success");

            Assert.That(command.CanExecute("success"), Is.True);
        }
        #endregion CanExecute
    }
}
