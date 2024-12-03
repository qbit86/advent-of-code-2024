package com.adventofcode2024

import kotlin.math.abs
import kotlin.math.sign

data class Report<TLevels>(val levels: TLevels, val excludedIndex: Int) where TLevels : List<Int> {

    companion object {
        fun <TLevels> create(levels: TLevels, excludedIndex: Int): Report<TLevels> where TLevels : List<Int> =
            Report(levels, excludedIndex)
    }

    private fun tryDecrement(index: Int): Int? {
        val candidate = index - 1
        return (if (candidate == excludedIndex) candidate - 1 else candidate).takeIf { it >= 0 }
    }

    private fun tryIncrement(index: Int): Int? {
        val candidate = index + 1
        return (if (candidate == excludedIndex) candidate + 1 else candidate).takeIf { it < levels.size }
    }

    fun tryGetLeftSignIfSafe(index: Int): Int? {
        val previousIndex = tryDecrement(index) ?: return 0
        val signedDifference = levels[index] - levels[previousIndex]
        return if (abs(signedDifference) in 1..3) signedDifference.sign else null
    }

    fun tryGetRightSignIfSafe(index: Int): Int? {
        val nextIndex = tryIncrement(index) ?: return 0
        val signedDifference = levels[nextIndex] - levels[index]
        return if (abs(signedDifference) in 1..3) signedDifference.sign else null
    }

}
