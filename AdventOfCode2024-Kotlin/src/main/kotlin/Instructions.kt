package com.adventofcode2024

interface IInstruction

object DoInstruction : IInstruction

object DontInstruction : IInstruction

data class MulInstruction(val left: MatchGroup, val right: MatchGroup) : IInstruction
