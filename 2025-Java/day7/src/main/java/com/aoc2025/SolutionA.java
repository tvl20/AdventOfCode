package com.aoc2025;

import java.util.List;

public class SolutionA {
    private char[][] manifold;

    public SolutionA(List<String> input) {
        manifold = new char[input.size()][];

        for (int i = 0, inputSize = input.size(); i < inputSize; i++) {
            manifold[i] = input.get(i).toCharArray();
        }

        for (int i = 0; i < manifold[0].length; i++) {
            if (manifold[0][i] == 'S') manifold[0][i] = '|';
        }
    }

    public long solve() {
        int result = propagateBeam(1, 0);

        for (char[] ca : manifold) {
            for (char c : ca) {
                System.out.print(c);
            }
            System.out.println();
        }

        return result;
    }

    private int propagateBeam(int layer, int splits) {
        if (layer >= manifold.length) return splits;

        for (int i = 0, layerSize = manifold[layer].length; i < layerSize; i++) {
            if (manifold[layer - 1][i] == '|') {
                if (manifold[layer][i] == '^') {
                    splits += 1;
                    if (i > 0) manifold[layer][i - 1] = '|'; // split to the left
                    if (i < manifold[layer].length - 1) manifold[layer][i + 1] = '|'; // split to the right
                } else {
                    manifold[layer][i] = '|'; // continue line through center
                }
            }
        }

        return propagateBeam(layer + 1, splits);
    }
}
