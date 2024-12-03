package com.adventofcode2024

import java.io.File
import kotlin.math.max

object PartTwoPuzzle {
    fun solve(path: String): Long {
        require(path.isNotEmpty()) { "Path cannot be null or empty" }
        val lines = File(path).readLines()
        return solve(lines)
    }

    private fun solve(rows: List<String>): Long {
        val reports = Helpers.parse(rows)
        return reports.count { isReportWeaklySafe(it) }.toLong()
    }

    private fun <TLevels> isReportWeaklySafe(levels: TLevels): Boolean where TLevels : List<Int> {
        if (levels.size <= 2) return true

        val report = Report.create(levels, Int.MIN_VALUE)
        for (i in levels.indices) {
            val leftSign = report.tryGetLeftSignIfSafe(i)
            val rightSign = report.tryGetRightSignIfSafe(i)
            val isSameSign = leftSign == 0 || rightSign == 0 || leftSign == rightSign
            val isSafe = leftSign != null && rightSign != null && isSameSign

            if (!isSafe) {
                return isReportStrictlySafe(levels, i - 2, i - 1) ||
                    isReportStrictlySafe(levels, i - 1, i) ||
                    isReportStrictlySafe(levels, i, i + 1)
            }
        }
        return true
    }

    private fun <TLevels> isReportStrictlySafe(
        levels: TLevels, start: Int, excludedIndex: Int
    ): Boolean where TLevels : List<Int> = isReportStrictlySafeUnchecked(levels, max(start, 0), excludedIndex)

    private fun <TLevels> isReportStrictlySafeUnchecked(
        levels: TLevels, start: Int, excludedIndex: Int
    ): Boolean where TLevels : List<Int> {
        require(start >= 0) { "Start index must be non-negative." }

        val count = levels.size - start
        if (count <= 1) return true

        val report = Report.create(levels, excludedIndex)
        for (i in start until levels.size) {
            if (i == excludedIndex) continue

            val leftSign = report.tryGetLeftSignIfSafe(i)
            val rightSign = report.tryGetRightSignIfSafe(i)
            val isSameSign = leftSign == 0 || rightSign == 0 || leftSign == rightSign
            val isSafe = leftSign != null && rightSign != null && isSameSign

            if (!isSafe) return false
        }
        return true
    }
}
