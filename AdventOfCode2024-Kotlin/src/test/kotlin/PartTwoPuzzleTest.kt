import com.adventofcode2024.PartTwoPuzzle
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource
import java.util.stream.Stream

class PartTwoPuzzleTest {

    @ParameterizedTest
    @MethodSource("inputProvider")
    fun solve_ShouldBeEqual(inputPath: String, expected: Long) {
        assertEquals(expected, PartTwoPuzzle.solve(inputPath))
    }

    companion object {
        @JvmStatic
        fun inputProvider(): Stream<Arguments> {
            return Stream.of(
                Arguments.of("../assets/sample.txt", 80L),
                Arguments.of("../assets/sample-3.txt", 1206L),
                Arguments.of("../assets/sample-4.txt", 236L),
                Arguments.of("../assets/sample-5.txt", 368L),
                Arguments.of("../assets/input.txt", 818286L)
            )
        }
    }
}
