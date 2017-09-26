const hi = name => `hi ${name}`;
const greeting = name => hi(name);
console.log(greeting('times'), 'this was redundantly function wrapped');

const greeting2 = hi;
console.log(greeting2('times'), 'the function wrap was redundant!');

