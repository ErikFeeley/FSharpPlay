const _ = require('ramda');

// compose!!! ta da!!!
// const compose = function compose(f, g) {
//   return function (x) {
//     return f(g(x));
//   };
// };

// const compose2 = (f, g) => x => f(g(x));

// const toUpperCase = x => _.toUpper(x);

// const exclaim = x => `${x} !`;

// const shout1 = _.compose(exclaim, toUpperCase);
// const shout2 = _.compose2(exclaim, toUpperCase);

// console.log(shout1('send in the clowns'), 'not arrow style');
// console.log(shout2('send in the clowns'), 'with arrows yo');

// // without compose
// const shout3 = x => exclaim(toUpperCase(x));

// // order of sequence matters
// // wat where reduce come from.
// const head = x => x[0];
// const reverse = _.reduce((acc, x) => [x].concat(acc), []);
// const last = _.compose(head, reverse);

// // the point is if we did last on a list we would have to compose as
// // (head, reverse) in order to get reverse to run before head
// // to technically get the first element because this compose
// // functionaly runs right to left

// // we could define it the other way, but the way it works right now
// // is much more close to the math version of this therefore we gonna
// // roll with it this way.

// // associativity true compose(f, compose(g, g)) == compose(compose(f, g), h)
// // so we can do either
// // _.compose(toUpperCase, _.compose(head, reverse));
// // or
// // _.compose(_.compose(toUpperCase, head), reverse);

// // so we have to hav ea variadic compose
// // and use whatever order we want
// // established libs like lodash underscore and ramdo will
// // have this variadic version of compose.

// // variadic compose usage
// const lastUpper = _.compose(toUpperCase, head, reverse);

// const lastUpperResult = lastUpper(['jumpkck', 'roundhouse', 'uppercut']);
// console.log(lastUpperResult, 'last upper result should be uppercut in uppercase');

// const loudLastUpper = compose(exclaim, toUpperCase, head, reverse);
// loudLastUpper(['jumpkick', 'roundhouse', 'uppercut']);
// // u know the drill.

// // anyways u can combo associative stuff al you want
// // moving on

// // pointfree style means never having to say your data.
// // aka functions that never mention the data upon which they operate.

// // not pointfree because we mention the data: word
// const snakeCase = word => word.toLowerCase().replace(/\s+/ig, '_');

// // pointfree
// const pointFreeSnakeCase = compose(replace(/\s+/ig, '_'), toLowerCase);

// const thisIsATest = pointFreeSnakeCase('What is this a real hting that will work');
// console.log(thisIsATest, 'wat');

// // again not point free
// const notPointFreeInitials = name => name.split(' ').map(compose(toUpperCase, head)).join(', ');

// //point free
// const initials = _.compose(_.join(', '), _.map(_.compose(toUpperCase, head)), _.split(' '));

// // debugging stuffs.
// // wrong give angry an array and partial map
// // const latin = compose(map, angry, reverse);

// // right
// // const latin = compose(map(angry), reverse);

// // make in impure trace function to help debug

const trace = _.curry((tag, x) => { console.log(tag, x); return x; });

// actually incorrect
const dasherize = _.compose(_.join('-'), _.toLower, _.split(' '), _.replace(/\s{2,}/ig, ' '));

const dasherizeWithTrace = _.compose(_.join('-'), _.map(_.toLower), trace('after split'), _.split(' '))

// ^ with taht we can find that we needed to map tolower since its working on an array

const fixedDasherize = _.compose(_.join('-'), _.map(_.toLower), _.split(' '), _.replace(/\s{2,}/ig, ' '));
