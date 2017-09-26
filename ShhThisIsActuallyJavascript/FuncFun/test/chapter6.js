const _ = require('ramda');

// imperative
const cars = [];
const makes = [];
for (let i = 0; i < cars.length; i++) {
  makes.push(cars[i].make);
}
// declaritive
const makes2 = cars.map(car => car.make);
// imperative
// var authenticate = function(form) {
//   var user = toUser(form);
//   return logIn(user);
// };

// // declarative
// var authenticate = compose(logIn, toUser);`

