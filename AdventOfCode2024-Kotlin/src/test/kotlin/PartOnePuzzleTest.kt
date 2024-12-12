import com.adventofcode2024.PartOnePuzzle
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource
import java.util.stream.Stream

class PartOnePuzzleTest {

    @ParameterizedTest
    @MethodSource("inputProvider")
    fun solve_ShouldBeEqual(inputPath: String, expected: Long) {
        assertEquals(expected, PartOnePuzzle.solve(inputPath))
    }

    companion object {
        @JvmStatic
        fun inputProvider(): Stream<Arguments> {
            return Stream.of(
                Arguments.of("../assets/sample.txt", 140L),
                Arguments.of("../assets/sample-2.txt", 772L),
                Arguments.of("../assets/sample-3.txt", 1930L),
                Arguments.of("../assets/input.txt", 1370100L)
            )
        }
    }
}
