export const queryKeys = {
  series: {
    tree: ['series', 'tree'] as const,
  },
  observations: {
    list: ({ seriesId, year }: { seriesId?: number; year?: number | null }) =>
      ['observations', { seriesId, year: year ?? null }] as const,
  },
};
