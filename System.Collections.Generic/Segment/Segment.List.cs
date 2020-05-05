namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        public Segment(IReadOnlyList<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.source = new SourceReadOnlyList(source);
            this.HasSource = true;
            this.Offset = 0;
            this.Count = source.Count;
        }

        public Segment(IReadOnlyList<T> source, int offset, int count)
        {
            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = new SourceReadOnlyList(source);
            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public Segment(IList<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.source = new SourceList(source);
            this.HasSource = true;
            this.Offset = 0;
            this.Count = source.Count;
        }

        public Segment(IList<T> source, int offset, int count)
        {
            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = new SourceList(source);
            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public static implicit operator Segment<T>(List<T> list)
            => list == null ? Empty : new Segment<T>(list as IList<T>);
    }
}