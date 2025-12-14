namespace CommonUtilitiesCSharp.Core
{
    /// <summary>
    /// Wrapper class for the execution of actions that require predicates to be met before executing.
    /// </summary>
    public class Command
    {
        #region Fields
        private readonly Func<object?, Task> _execute;
        private readonly Predicate<object?> _canExecute = param => true;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes an instance of a synchronous command that will always execute.
        /// </summary>
        /// <param name="execute">Action to execute when the command is triggered</param>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="execute"/> is null.</exception>
        public Command(Action<object?> execute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _execute = param => { execute(param); return Task.CompletedTask; };
        }

        /// <summary>
        /// Initializes an instance of a synchronous command that will only execute when the predicate is met.
        /// </summary>
        /// <param name="execute">Action to execute when the command is triggered.</param>
        /// <param name="canExecute">Predicate that must be met for the command to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="execute"/> or <paramref name="canExecute"/> is null.</exception>
        public Command(Action<object?> execute, Predicate<object?> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _execute = param => { execute(param); return Task.CompletedTask; };
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// Initializes an instance of an asynchronous command that will always execute.
        /// </summary>
        /// <param name="execute">Asynchronous action to execute when the command is triggered.</param>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="execute"/> is null.</exception>
        public Command(Func<object?, Task> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes an instance of an asynchronous command that will only execute when the predicate is met.
        /// </summary>
        /// <param name="execute">Asynchronous action to execute when the command is triggered.</param>
        /// <param name="canExecute">Predicate that must be met for the command to execute.</param>
        /// <exception cref="ArgumentNullException">Thrown is <paramref name="execute"/> or <paramref name="canExecute"/> is null.</exception>
        public Command(Func<object?, Task> execute, Predicate<object?> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }
        #endregion Constructors

        #region Members
        /// <summary>
        /// Returns true if the command is allowed to execute, taking into account the given parameter.
        /// </summary>
        /// <param name="parameter">Parameter to be used by the predicate to determine if the command can be executed.</param>
        /// <returns>Returns true if the command is allowed to execute.</returns>
        public bool CanExecute(object? parameter = null)
        {
            return _canExecute(parameter);
        }

        /// <summary>
        /// Attempts to execute the command, using the given parameter. If the command is not allowed to execute, nothing happens.
        /// </summary>
        /// <param name="parameter">Parameter to be used by the predicate to determine if the command can be executed and the execution logic itself if the command is allowed to execute.</param>
        public async Task Execute(object? parameter = null)
        {
            if (CanExecute(parameter))
                await _execute(parameter);

            return;
        }
        #endregion Members

        /// <summary>
        /// Converts the given parameter to the expected type and throw a standard exception if the conversion fails.
        /// </summary>
        /// <typeparam name="TParameter">Type to cast the given parameter to.</typeparam>
        /// <param name="commandParameter">Parameter to cast to the designated type.</param>
        /// <returns>Returns <paramref name="commandParameter"/> cast to the type <typeparamref name="TParameter"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="commandParameter"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown is <paramref name="commandParameter"/> cannot be cast to type <typeparamref name="TParameter"/>.</exception>
        public static TParameter ConvertParameter<TParameter>(object? commandParameter)
        {
            ArgumentNullException.ThrowIfNull(commandParameter);

            if (commandParameter is TParameter expectedParameter)
            {
                return expectedParameter;
            }
            else
            {
                throw new ArgumentException($"Parameter \"{commandParameter}\" is of type \"{commandParameter.GetType().Name}\" rather than expected type \"{typeof(TParameter).Name}\"", nameof(commandParameter));
            }
        }
    }

    /// <summary>
    /// Wrapper class for the execution of actions that require predicates to be met before executing. Command paramter is automatically cast to the expected type.
    /// </summary>
    /// <typeparam name="TParameter">Expected command parameter type.</typeparam>
    public class Command<TParameter> : Command
    {
        #region Private Methods
        private static void ConvertExecute(Action<TParameter?> action, object? commandParameter) => action(ConvertParameter(commandParameter));

        private static Task ConvertExecute(Func<TParameter?, Task> action, object? commandParameter) => action(ConvertParameter(commandParameter));

        private static bool ConvertCanExecute(Predicate<TParameter?> predicate, object? commandParameter) => predicate(ConvertParameter(commandParameter));

        private static TParameter? ConvertParameter(object? commandParameter) => ConvertParameter<TParameter?>(commandParameter);
        #endregion Private Methods

        #region Constructors
        /// <inheritdoc cref="Command(Action{object?})"/>
        public Command(Action<TParameter?> execute) : base(commandParameter => ConvertExecute(execute, commandParameter))
        {
            ArgumentNullException.ThrowIfNull(execute); // Avoids propagating a forced null value down the chain
        }

        /// <inheritdoc cref="Command(Action{object?}, Predicate{object?})"/>
        public Command(Action<TParameter?> execute, Predicate<TParameter?> canExecute) : base(
            commandParameter => ConvertExecute(execute, commandParameter),
            commandParameter => ConvertCanExecute(canExecute, commandParameter))
        {
            ArgumentNullException.ThrowIfNull(execute); // Avoids propagating a forced null value down the chain
            ArgumentNullException.ThrowIfNull(canExecute); // Avoids propagating a forced null value down the chain
        }

        /// <inheritdoc cref="Command(Func{object?, Task})"/>
        public Command(Func<TParameter?, Task> execute) : base(commandParameter => ConvertExecute(execute, commandParameter))
        {
            ArgumentNullException.ThrowIfNull(execute); // Avoids propagating a forced null value down the chain
        }

        /// <inheritdoc cref="Command(Func{object?, Task}, Predicate{object?})"/>
        public Command(Func<TParameter?, Task> execute, Predicate<TParameter?> canExecute) : base(
            commandParameter => ConvertExecute(execute, commandParameter),
            commandParameter => ConvertCanExecute(canExecute, commandParameter))
        {
            ArgumentNullException.ThrowIfNull(execute); // Avoids propagating a forced null value down the chain
            ArgumentNullException.ThrowIfNull(canExecute); // Avoids propagating a forced null value down the chain
        }
        #endregion Constructors

        #region Members
        /// <inheritdoc cref="Command.CanExecute(object?)"/>
        public bool CanExecute(TParameter? parameter)
        {
            return base.CanExecute(parameter);
        }

        /// <inheritdoc cref="Command.Execute(object?)"/>
        public async Task Execute(TParameter? parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            await base.Execute(parameter);
        }
        #endregion Members
    }
}
