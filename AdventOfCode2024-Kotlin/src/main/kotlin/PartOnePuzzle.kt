package com.adventofcode2024

import java.io.File

object PartOnePuzzle {
    private const val PATTERN = "XMAS"
    private const val REVERSED_PATTERN = "SAMX"
    private const val PATTERN_LENGTH = 4

    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty." }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun <TRows> solve(rows: TRows): Long where TRows : List<String> {
        var horizontalCount = 0L
        var verticalCount = 0L
        var diagonalCount = 0L
        var antiDiagonalCount = 0L

        for (i in rows.indices) {
            for (j in rows[i].indices) {
                horizontalCount += if (hasHorizontal(rows, i, j)) 1 else 0
                verticalCount += if (hasVertical(rows, i, j)) 1 else 0
                diagonalCount += if (hasDiagonal(rows, i, j)) 1 else 0
                antiDiagonalCount += if (hasAntiDiagonal(rows, i, j)) 1 else 0
            }
        }

        return horizontalCount + verticalCount + diagonalCount + antiDiagonalCount
    }

    private fun <TRows> hasHorizontal(
        rows: TRows,
        rowIndex: Int,
        columnIndex: Int
    ): Boolean where TRows : List<String> {
        val row = rows[rowIndex]
        if (columnIndex + PATTERN_LENGTH > row.length) return false
        val span = row.substring(columnIndex, columnIndex + PATTERN_LENGTH)
        return span == PATTERN || span == REVERSED_PATTERN
    }

    private fun <TRows> hasVertical(rows: TRows, rowIndex: Int, columnIndex: Int): Boolean where TRows : List<String> {
        if (rowIndex + PATTERN_LENGTH > rows.size) return false
        val span = CharArray(PATTERN_LENGTH)

        for (i in 0 until PATTERN_LENGTH) {
            span[i] = rows[rowIndex + i][columnIndex]
        }

        return String(span) == PATTERN || String(span) == REVERSED_PATTERN
    }

    private fun <TRows> hasDiagonal(rows: TRows, rowIndex: Int, columnIndex: Int): Boolean where TRows : List<String> {
        if (columnIndex + PATTERN_LENGTH > rows[rowIndex].length || rowIndex + PATTERN_LENGTH > rows.size) return false

        val span = CharArray(PATTERN_LENGTH)

        for (i in 0 until PATTERN_LENGTH) {
            span[i] = rows[rowIndex + i][columnIndex + i]
        }

        return String(span) == PATTERN || String(span) == REVERSED_PATTERN
    }

    private fun <TRows> hasAntiDiagonal(
        rows: TRows,
        rowIndex: Int,
        columnIndex: Int
    ): Boolean where TRows : List<String> {
        if (columnIndex + PATTERN_LENGTH > rows[rowIndex].length || rowIndex + PATTERN_LENGTH > rows.size) return false

        val span = CharArray(PATTERN_LENGTH)

        for (i in 0 until PATTERN_LENGTH) {
            span[i] = rows[rowIndex + PATTERN_LENGTH - 1 - i][columnIndex + i]
        }

        return String(span) == PATTERN || String(span) == REVERSED_PATTERN
    }
}
