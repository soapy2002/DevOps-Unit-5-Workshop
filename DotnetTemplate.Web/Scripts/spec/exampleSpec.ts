import { functionOne } from '../home/example';

describe('Example', () => {
  it('can run functionOne', () => {
    // Given
    const one = 1;

    // When
    functionOne();

    // Then
    expect(1).toEqual(one);
  });
});
