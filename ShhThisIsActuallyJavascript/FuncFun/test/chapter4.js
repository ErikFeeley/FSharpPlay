// currying is like microwaves... once you have em
// you can't live without em.

const add = function add(x) {
  return function inner(y) {
    return x + y;
  };
};

const add2 = x => y => x + y;

const incrementTest = add2(1);
console.log(incrementTest(2), 'test should be 3');
console.log(add2(1)(2), 'the parens! madness!');

const increment = add(1);
const addTen = add(10);

console.log(increment(2), 'should be 3 curry test');
console.log(addTen(2), 'should be 12 curry test');

// curry function makes it nicer to define and call curried functions.
const curry = require('lodash/curry');

// yes were just wrapping stuff
// but it shows us what we can do with currying.
// and building up functions.
const match = curry((what, str) => str.match(what));
const replace = curry((what, replacement, str) => str.replace(what, replacement));
const filter = curry((f, ary) => ary.filter(f));
const map = curry((f, ary) => ary.map(f));

// see how we can call them.
match(/\s+/g, 'hello world');
match(/\s+g/)('hello world');

const hasSpaces = match(/\s+/g);
// is a = (x) => x.match(etc..);
hasSpaces('hello world');
hasSpaces('spaceless');

filter(hasSpaces, ['tori_spelling', 'tori amos']);
// ['tori amos']

const findSpaces = filter(hasSpaces);
// function (xs) (return xs.filter(function (x) { return x.match... etc}));

findSpaces(['tori_spelling', 'tori amos']);
// ['tori amos']

const noVowles = replace(/[aeiouy]/ig);
// function (replacement, x) {return x.replace(that regix), replacement);

const censored = noVowles('*');

console.log(censored('Chocolate Rain'));

const getChildren = x => x.childNodes;

// transform function that works on single elmeents
// into a function that works on array simply by wrapping wiht map
const allTheChildren = map(getChildren);

// exercises
const _ = require('ramda');

// Exercise 1
//= =============
// Refactor to remove all arguments by partially applying the function.

const words = _.split(' ');

// Exercise 1a
//= =============
// Use map to make a new words fn that works on an array of strings.

const sentences = _.map(words);


// Exercise 2
//= =============
// Refactor to remove all arguments by partially applying the functions.

let filterQs = _.filter(match(/q/i));


// Exercise 3
//= =============
// Use the helper function _keepHighest to refactor max to not reference any
// arguments.

// LEAVE BE:
let _keepHighest = function (x, y) {
  return x >= y ? x : y;
};

// REFACTOR THIS ONE:
let max = _.reduce(_keepHighest, -Infinity);


// Bonus 1:
// ============
// Wrap array's slice to be functional and curried.
// //[1, 2, 3].slice(0, 2)
let slice = _.curry((start, end, xs) => xs.slice(start, end));


// Bonus 2:
// ============
// Use slice to define a function "take" that returns n elements from the beginning of an array. Make it curried.
// For ['a', 'b', 'c'] with n=2 it should return ['a', 'b'].
let take = slice(0);