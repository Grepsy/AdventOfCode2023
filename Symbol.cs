public class Symbol {
    private static Dictionary<string, Symbol> _symbols = [];

    public string Value { get; }

    public Symbol(string value) {
        Value = value;
    }

    public static Symbol New(string value) {
        if (!_symbols.TryGetValue(value, out var result)) {
            result = _symbols[value] = new(value);
        }

        return result;
    }

    public static Symbol New(char value) => New(value.ToString());
}
