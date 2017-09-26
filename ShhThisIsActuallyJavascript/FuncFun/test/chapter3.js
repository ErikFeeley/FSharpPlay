const xs = [1, 2, 3, 4, 5];

// pure
xs.slice(0, 3);
// => [1, 2, 3]

xs.slice(0, 3);
// => still [1, 2, 3]

xs.slice(0, 3);
// => still [1, 2, 3]

// impure
xs.splice(0, 3);
// => [1, 2, 3]

xs.splice(0, 3);
// shit => [4, 5]

xs.splice(0, 3);
// welp => []

// impure
const min = 21;

const checkAge = age => age >= min;

// pure
const checkAgePure = (age) => {
  const minPure = 21;
  return age >= minPure;
};


// cases for pure functions... they can always be cached!

const memoize = (f) => {
  const cache = {};

  return () => {
    const argStr = JSON.stringify(arguments);
    cache[argStr] = cache[argStr] || f.apply(f, arguments);
    return cache[argStr];
  };
};

const squareNumber = () => memoize(x => x * x);

const squareNumber2 = memoize(function (x) {
  return x * x;
});

// not sure y this does not work
squareNumber(4);
console.log(squareNumber2(5), 'memoized it remembers');

const Immutable = require('immutable');

const jobe = Immutable.Map({
  name: 'Jobe',
  hp: 20,
  team: 'red'
});
const michael = Immutable.Map({
  name: 'Michael',
  hp: 20,
  team: 'green'
});

const decrementHp = player => player.set('hp', player.get('hp') - 1);
const isSameTeam = (player1, player2) => player1.get('team') === player2.get('team');
const punch = (player, target) => (isSameTeam(player, target) ? target : decrementHp(target));

console.log(punch(jobe, michael), 'punching');

// illustrate how the punch function breaks down.
// shows how easy it is to reason about these things.
const punch2 = (player, target) => (player.get('team') === target.get('team') ? target : decrementHp(target));
const punch3 = (player, target) => ('red' === 'green' ? target : decrementHp(target));
const punch4 = (player, target) => decrementHp(target);
const punch5 = (player, target) => target.set('hp', target.get('hp') - 1);
