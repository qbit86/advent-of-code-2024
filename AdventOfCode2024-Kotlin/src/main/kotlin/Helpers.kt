package com.adventofcode2024

import kotlin.math.floor
import kotlin.math.log10
import kotlin.math.pow

object Helpers {

    fun solve(line: String, blinkCount: Int): Long {
        val stones = line.split(' ').map { it.toLong() }
        return solve(stones.toLongArray(), blinkCount)
    }

    private fun solve(stones: LongArray, blinkCount: Int): Long {
        if (blinkCount < 1) return stones.size.toLong()

        val stoneCounts = stones.map { compute(mutableMapOf(), it, blinkCount) }
        return stoneCounts.sum()
    }

    private fun getFromCacheOrCompute(cache: MutableMap<Node, Long>, stone: Long, remainingBlinks: Int): Long {
        val node = Node(stone, remainingBlinks)
        return cache[node] ?: run {
            val result = compute(cache, stone, remainingBlinks)
            cache[node] = result
            result
        }
    }

    private fun compute(cache: MutableMap<Node, Long>, stone: Long, remainingBlinks: Int): Long {
        if (remainingBlinks == 0) return 1

        if (stone == 0L) return getFromCacheOrCompute(cache, 1, remainingBlinks - 1)

        val digitCount = computeDigitCount(stone)
        if (digitCount % 2 == 0) {
            val newDigitCount = digitCount / 2
            val divisor = 10.0.pow(newDigitCount).toLong()
            val quotient = stone / divisor
            val remainder = stone % divisor
            return getFromCacheOrCompute(cache, quotient, remainingBlinks - 1) +
                getFromCacheOrCompute(cache, remainder, remainingBlinks - 1)
        }

        return getFromCacheOrCompute(cache, stone * 2024, remainingBlinks - 1)
    }

    private fun computeDigitCount(n: Long): Int = if (n == 0L) 1 else (floor(log10(n.toDouble())) + 1).toInt()
}
