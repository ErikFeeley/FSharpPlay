// song with words
// isert certain number of words "WUB" before the first word of the song
// and between words

const result = 'A B C'.split(' ').reduce((prev, curr) => `${prev}WUB${curr}`);


console.log('AWUBWUBWUBBWUBWUBWUBC'.replace(/(WUB)+/g, ' ').trim());


const songDecoder = song => song.replace(/(WUB)+/g, ' ').trim();