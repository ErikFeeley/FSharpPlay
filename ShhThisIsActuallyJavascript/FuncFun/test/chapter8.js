const _ = require('ramda');

const Container = function Container(x) {
  this.value = x;
};

Container.of = x => new Container(x);

console.log(Container.of(3), 'of 3');

console.log(Container.of('hotdogs'), 'of hotdogs');

console.log(Container.of(Container.of({
  name: 'yoda'
})));

// my first functor
// once our value is in the container we wil lneed a way to run
// functions on it.

// (a -> b) -> Container a -> Container b
Container.prototype.map = function map(f) {
  return Container.of(f(this.value));
};

console.log(Container.of(2).map(two => two + 2), 'this works now, could not do prototype thing again cuz stupid this');

console.log(Container.of('flamethrowers').map(s => s.toUpperCase()), 'to upper map flamethrowers');

const thirdContainerResult = Container.of('bombs')
  .map(_.concat(' away'))
  .map(_.prop('length'));

console.log(thirdContainerResult);

// what the maybe would look like as a class
// seems to work the same
// class Maybe {
//   constructor(x) {
//     this.value = x;
//   }
//   isNothing() {
//     return (this.value === null || this.value === undefined);
//   }
//   map(f) {
//     return this.isNothing() ? Maybe.of(null) : Maybe.of(f(this.value));
//   }
//   // no proto or this stuff going on here
//   // safe to use arrow.
//   static of(x) { return new Maybe(x); }
// }

const Maybe = function Maybe(x) {
  this.value = x;
};

// no proto or this stuff going on here
// safe to use arrow.
Maybe.of = x => new Maybe(x);

Maybe.prototype.isNothing = function isNothing() {
  return (this.value === null || this.value === undefined);
};

Maybe.prototype.map = function map(f) {
  return this.isNothing() ? Maybe.of(null) : Maybe.of(f(this.value));
};

// oh ho hoo now we can play with nothing! yay!
// side note this is a simple Maybe implementation.

const firstMaybeResult = Maybe.of('Malkovich Malkovich').map(_.match(/a/ig));
console.log(firstMaybeResult);

const secondMaybeResult = Maybe.of(null).map(_.match(/a/ig));
console.log(secondMaybeResult, 'maybe of null map match regex');
// => maybe { value: null } but eyyy no errors

const thirdMaybeResult = Maybe.of({
  name: 'Boris',
}).map(_.prop('age'))
  .map(_.add(10));
console.log(thirdMaybeResult, 'maybe of object name no age map on age and stuff');
// => maybe { value: null } but eyy agian no errors on the value;

const fourthMaybeResult = Maybe.of({
  name: 'Dinah',
  age: 14
}).map(_.prop('age')).map(_.add(10));
console.log(fourthMaybeResult, 'fourth maybe actually has age to map on');

// eyyy app doesnt explode even though we map on functions with nul values.
// maybe take scare of it.
// hmm lets try pointfree. lets check this shit.

// map :: functor f => (a -> b) -> f a -> f b

const map = _.curry((f, anyFunctorAtAll) => anyFunctorAtAll.map(f));
// can carry on with composition per usual and map wil work.

// use cases
// in the wild we see Maybe used in functions
// which might fail to return a result.

// safeHead :: [a] -> Maybe(a)
const safeHead = xs => Maybe.of(xs[0]);

const streetName = _.compose(
  map(_.prop('street')),
  safeHead, _.prop('addresses')
);

console.log(streetName({
  addresses: []
}));
// Maybe(null)

console.log(streetName({
  addresses: [{
    street: 'Shady Ln.',
    number: 4201
  }]
}));
// Maybe('Shady Ln.')

// safehead is like normal head but with added type safety

// withdraw :: Number -> Account -> Maybe(Account)

const withdraw = _.curry((amount, account) => (account.balance >= amount ?
  Maybe.of({
    balance: account.balance - amount
  }) :
  Maybe.of(null)));

// chapter had incomplete function defs for the next example
// point is can return a maybe of null and use it
// without fear of null blowing everything up.

// release the value.

// so we cannot exactly get at things with return sooooo
// shocking we must run some function to get it back
// out into the world :)

// linear flow despite logical branching.

// maybe :: b -> (a -> b) -> Maybe a -> b
// thats a mouthfull.
const maybe = _.curry((x, f, m) => (m.isNothing() ? x : f(m.value)));

// examples from dr boolean

// //  getTwenty :: Account -> String
// var getTwenty = compose(
//   maybe("You're broke!", finishTransaction), withdraw(20)
// );


// getTwenty({
//   balance: 200.00,
// });
// // "Your balance is $180.00"

// getTwenty({
//   balance: 10.00,
// });
// // "You're broke!"

// see we supply some kind of default to the maybe

// pure error handling

const Left = function Left(x) {
  this.value = x;
};

Left.of = function leftOf(x) {
  return new Left(x);
};

Left.prototype.map = function leftMap(f) {
  return this;
};

const Right = function Right(x) {
  this.value = x;
};

Right.of = function rightOf(x) {
  return new Right(x);
};

Right.prototype.map = function rightMap(f) {
  return Right.of(f(this.value));
};

// left and right are two subclasses of a super
// type either just skippint eh ceremony
// of creatiing it here.

const rightRainMap = Right.of('rain').map(str => `b${str}`);
console.log(rightRainMap);
// Right('brain')

// gah console.log takes to long try dis
const qlog = console.log;

const leftRainMap = Left.of('rain').map(str => `b${str}`);
qlog(leftRainMap);
// Left('rain')

const rightHostMap = Right.of({
  host: 'localhost',
  port: 80
}).map(_.prop('host'));
qlog(rightHostMap);
// Right('localhost');

const leftRollEyeMap = Left.of('rolls eyes...').map(_.prop('host'));
qlog(leftRollEyeMap);
// Left('rols eyes...')

// left ignores our request ot map over it, however
// right works just like our container, power comes from
// embeding erro rmessgae within left.

// so what if function tha tmight not succeed.
// cna use maybe(null) to signal failure
// and branch our program, however doesnt tell us uch.
// maybe we wana know why it failed.
// lets do it with either.

const moment = require('moment');

// getAge :: Date -> User -> Either(String, Number)
const getAge = _.curry((now, user) => {
  const birthDate = moment(user.birthDate, 'YYYY-MM-DD');
  if (!birthDate.isValid()) return Left.of('Birth date could not be parsed');
  return Right.of(now.diff(birthDate, 'years'));
});

const getAgeResult = getAge(moment(), {
  birthDate: '2005-12-12'
});
console.log(getAgeResult, 'has result');

const getAgeResult2 = getAge(moment(), {
  birthDate: 'July 4, 2001'
});
console.log(getAgeResult2, 'has error message from Left.of');

// fortune :: Number -> String
const fortune = _.compose(_.concat('If you survive, you will be '), _.add(1));

// zoltar :: User -> Either(String, _)
const zoltar = _.compose(_.map(console.log), _.map(fortune), getAge(moment()));

const zoltarResult = zoltar({
  bDate: '2005-12-12'
});
qlog(zoltarResult, 'if you survie u will be... etc '); // not werk wat?

const zoltarResult2 = zoltar({
  bDate: 'balloons!'
});
qlog(zoltarResult2, 'eyyy');

// either :: (a -> c) -> (b -> c) -> Either a  b -> c
const bestEither = _.curry((f, g, e) => {
  switch (e.constructor) {
    case Left:
      return f(e.value);
    case Right:
      return g(e.value);
  }
});

// bestZoltar :: User -> _
const bestZoltar = _.compose(console.log, bestEither(_.identity, fortune), getAge(moment()));

const bestZoltarResult = bestZoltar({
  datDate: '2005-12-12' // hmm getting birth date could not be parsed..
});