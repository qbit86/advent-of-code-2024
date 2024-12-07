package com.adventofcode2024

import java.io.File

object PartTwoPuzzle {
    private const val PATTERN = "MS"
    private const val REVERSED_PATTERN = "SM"
    private const val WINDOW_SIZE = 3

    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty." }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun <TRows> solve(rows: TRows): Long where TRows : List<String> {
        var count = 0
        val horizontalBound = rows.size - WINDOW_SIZE + 1

        for (i in 0 until horizontalBound) {
            val verticalBound = rows[i].length - WINDOW_SIZE + 1
            for (j in 0 until verticalBound) {
                count += if (containsPattern(rows, i, j)) 1 else 0
            }
        }

        return count.toLong()
    }

    private fun <TRows> containsPattern(
        rows: TRows,
        rowIndex: Int,
        columnIndex: Int
    ): Boolean where TRows : List<String> {
        if (rows[rowIndex + 1][columnIndex + 1] != 'A') return false

        val span = CharArray(PATTERN.length)
        span[0] = rows[rowIndex][columnIndex]
        span[1] = rows[rowIndex + 2][columnIndex + 2]

        if (String(span) != PATTERN && String(span) != REVERSED_PATTERN) return false

        span[0] = rows[rowIndex + 2][columnIndex]
        span[1] = rows[rowIndex][columnIndex + 2]

        return String(span) == PATTERN || String(span) == REVERSED_PATTERN
    }
}
