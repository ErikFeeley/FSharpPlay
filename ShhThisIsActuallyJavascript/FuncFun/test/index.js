// drBoolean example stuff chaper 1.
// except converting it to more es6ish style
class Flock {
  constructor(n) {
    this.seagulls = n;
  }

  conjoin(other) {
    this.seagulls += other.seagulls;
    return this;
  }

  breed(other) {
    this.seagulls = this.seagulls * other.seagulls;
    return this;
  }
}

const flockA = new Flock(4);
const flockB = new Flock(2);
const flockC = new Flock(0);

const result = flockA.conjoin(flockC)
  .breed(flockB).conjoin(flockA.breed(flockB)).seagulls;

console.log(result);

const conjoin = function conjoin(flockX, flockY) { return flockX + flockY; };
const breed = function breed(flockX, flockY) { return flockX * flockY; };

const flockE = 4;
const flockF = 2;
const flockG = 0;

const result2 = conjoin(breed(flockF, conjoin(flockE, flockG)), breed(flockE, flockF));

console.log(result2, 'more functional result');

// reveal the true identity.
const add = (x, y) => x + y;
const multiply = (x, y) => x * y;

const flockAlpha = 4;
const flockBeta = 2;
const flockCharlie = 0;

const trueResult = add(
  multiply(flockBeta, add(flockAlpha, flockCharlie)),
  multiply(flockAlpha, flockBeta)
);

console.log(trueResult, 'the true result');

// knowledge of the encients
// add(add(x, y), z) === add(x, add(y, z));
// add(x, y) === add(y, x)
// add(x, 0) === x;
// multiply(x, add(y,z)) === add(multipley(x, y), multipely(x, z));

const ancientKnowledgeResult = multiply(flockBeta, add(flockAlpha, flockAlpha));

console.log(ancientKnowledgeResult, 'ancient knowledge');