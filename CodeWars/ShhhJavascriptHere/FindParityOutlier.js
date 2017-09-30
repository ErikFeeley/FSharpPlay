const findOutlier = (integers) => {
  const evens = integers.filter(x => x % 2 === 0);
  const odds = integers.filter(x => x % 2 !== 0);

  return evens.length > odds.length ? odds.pop() : evens.pop();
};

const result = findOutlier([2, 6, 8, 10, 3]);
console.log(result);


const findOutlierBetter = (integers) => {
  const evens = [];
  const odds = [];
  integers.forEach(x => (x % 2 === 0 ? evens.push(x) : odds.push(x)));
  return evens.length === 1 ? evens[0] : odds[0];
};

[1, 2, 3].reduce((prev, curr) => {
  const prevIsEven = prev % 2 === 0;
  const currIsEven = curr % 2 === 0;
  console.log(`${prev} prev ${prevIsEven} prev is even ${curr} curr ${currIsEven} cur is even`);
  return prevIsEven && currIsEven ? prev : curr;
});

const findOutlierBest = (integers) => {

};