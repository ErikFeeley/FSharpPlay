const _ = require('ramda');

// hindley-milner type signatures
// capitalize :: String -> String
const capitalize = s => _.toUpper(_.head(s)) + _.toLower(_.tail(s));

capitalize('smurf');
// => Smurf

// strLength :: String -> Number
const strLength = s => s.length;

console.log(strLength('eywhatup'));

// join :: String -> [String] -> String
const join = _.curry((what, xs) => xs.join(what));

console.log(join('wat', ['ey', 'cool']));

// match :: Regex -> (String -> [String])
const match = _.curry((reg, s) => s.match(reg));

// onHolidah :: String -> [String]
const onHoliday = match(/holiday/ig);

// replace :: Regex -> (String -> (String -> String))
const replace = _.curry((reg, sub, s) => s.replace(reg, sub));

// id:: a -> a
const id = x => x;

// map :: (a -> b) -> [a] -> [b]
const map = _.curry((f, xs) => xs.map(f));

// head :: [a] -> a
const head = xs => xs[0];

// filter :: (a -> Bool) -> [a] -> [a]
const filter = _.curry((f, xs) => xs.filter(f));

// reduce :: (b -> a -> b) -> b -> [a] -> b
const reduce = _.curry((f, x, xs) => xs.reduce(f, x));