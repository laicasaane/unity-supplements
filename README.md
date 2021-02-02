# Unity Supplements

## Changelog

### 2.5.3

#### Breaking changes
- Change the namespace of all pooling-related classes

## Global

### Extensions
- [Array1](./Global/Extensions/Array1Extensions.cs)
- [Number](./Global/Extensions/NumberExtensions.cs)
- [String](./Global/Extensions/StringExtensions.cs)

## System

### Delegate
- [ReadStructAction](./System/Delegates/ReadStructAction.cs)
- [ReadStructFunc](./System/Delegates/ReadStructFunc.cs)
- [ReadStructPredicate](./System/Delegates/ReadStructPredicate.cs)
- [RefAction](./System/Delegates/RefAction.cs)
- [RefFunc](./System/Delegates/RefFunc.cs)
- [RefPredicate](./System/Delegates/RefPredicate.cs)

### Interface
- [IComparableIn\<T>](./System/Interfaces/IComparableIn%7BT%7D.cs)
- [IComparableReadOnlyStruct\<T>](./System/Interfaces/IComparableReadOnlyStruct%7BT%7D.cs)
- [IEquatableIn\<T>](./System/Interfaces/IEquatableIn%7BT%7D.cs)
- [IEquatableReadOnlyStruct\<T>](./System/Interfaces/IEquatableReadOnlyStruct%7BT%7D.cs)

### Index & Length
- [Index2](./System/Indices/Index2.cs)
- [Index3](./System/Indices/Index3.cs)
- [Index4](./System/Indices/Index4.cs)
- [Index5](./System/Indices/Index5.cs)
- [Length2](./System/Lengths/Length2.cs)
- [Length3](./System/Lengths/Length3.cs)
- [Length4](./System/Lengths/Length4.cs)
- [Length5](./System/Lengths/Length5.cs)

### Array
- [ReadArray1\<T>](./System/ReadArray1/ReadArray1%7BT%7D.cs)
- [ReadArray1\<T> extensions](./System/ReadArray1/ReadArray1TExtensions.cs)

### Enum
- [Enum\<T>](./System/Enum/Enum%7BT%7D.cs)
- [EnumIndex](./System/Enum/EnumIndex.cs)
- [EnumLength](./System/Enum/EnumLength.cs)
- [EnumRandomizer\<T>](./System/Randomizer/EnumRandomizer%7BT%7D.cs)

### Singleton
- [Singleton](./System/Singletons/Singleton.cs)
- [Singleton\<T>](./System/Singletons/Singleton%7BT%7D.cs)

### Pseudo Probability
- [PseudoProbability](./System/PseudoProbability/PseudoProbability.cs)
- [PseudoProbability.PRD](./System/PseudoProbability/PseudoProbability.PRD.cs)
- [PseudoProbability.IMath](./System/PseudoProbability/PseudoProbability.IMath.cs)
- [PseudoProbability.IRandom](./System/PseudoProbability/PseudoProbability.IRandom.cs)

### Range
- [IRange\<T>](./System/Range/IRange%7BT%7D.cs)
- [IRange\<TValue, TEnumerator>](./System/Range/IRange%7BTValue%2CTEnumerator%7D.cs)
- [IRangeEnumerator\<T>](./System/Range/IRangeEnumerator%7BT%7D.cs)
- [ReadRange\<T>](./System/Range/ReadRange%7BT%7D.cs)
- [ReadRange\<TValue, TEnumerator>](./System/Range/ReadRange%7BTValue%2CTEnumerator%7D.cs)
- [ByteRange](./System/Range/ByteRange.cs)
- [SByteRange](./System/Range/SByteRange.cs)
- [ShortRange](./System/Range/ShortRange.cs)
- [UShortRange](./System/Range/UShortRange.cs)
- [IntRange](./System/Range/IntRange.cs)
- [UIntRange](./System/Range/UIntRange.cs)
- [LongRange](./System/Range/LongRange.cs)
- [ULongRange](./System/Range/ULongRange.cs)
- [CharRange](./System/Range/CharRange.cs)
- [EnumByteRange\<T>](./System/Range/EnumByteRange%7BT%7D.cs)
- [EnumSByteRange\<T>](./System/Range/EnumSByteRange%7BT%7D.cs)
- [EnumShortRange\<T>](./System/Range/EnumShortRange%7BT%7D.cs)
- [EnumUShortRange\<T>](./System/Range/EnumUShortRange%7BT%7D.cs)
- [EnumIntRange\<T>](./System/Range/EnumIntRange%7BT%7D.cs)
- [EnumUIntRange\<T>](./System/Range/EnumUIntRange%7BT%7D.cs)
- [EnumLongRange\<T>](./System/Range/EnumLongRange%7BT%7D.cs)
- [EnumULongRange\<T>](./System/Range/EnumULongRange%7BT%7D.cs)
- [EnumRange\<T>](./System/Range/EnumRange%7BT%7D.cs)

### Misc.
- [Converter](./System/Converter.cs)

## System.Delegates

- [IAction](./System.Delegates/Action/IAction.cs)
- [IActionIn](./System.Delegates/Action/IActionIn.cs)
- [IActionRef](./System.Delegates/Action/IActionRef.cs)
- [IFunc](./System.Delegates/Func/IFunc.cs)
- [IFuncIn](./System.Delegates/Func/IFuncIn.cs)
- [IFuncRef](./System.Delegates/Func/IFuncRef.cs)
- [IPredicate](./System.Delegates/Predicate/IPredicate.cs)
- [IPredicateIn](./System.Delegates/Predicate/IPredicateIn.cs)
- [IPredicateRef](./System.Delegates/Predicate/IPredicateRef.cs)

## System.ValueDelegates

- [ValueAction](./System.ValueDelegates/Action)
- [ValueFunc](./System.ValueDelegates/Func)
- [ValuePredicate](./System.ValueDelegates/Predicate)
- [ValueDelegate](./System.ValueDelegates/Delegate)

## System.IO

- [FileSystem](./System.IO/FileSystem.cs)

## System.Runtime.Serialization

- [SerializationInfoExtensions](./System.Runtime.Serialization/SerializationInfoExtensions.cs)

## System.Runtime.Serialization.Formatters.Text

- [ITextFormatter](./System.Runtime.Serialization.Formatters.Text/ITextFormatter.cs)
- [ITextFormatter\<T>](./System.Runtime.Serialization.Formatters.Text/ITextFormatter%7BT%7D.cs)

## System.Collections.Generics

### Interface
- [IEqualityComparerIn\<T>](./System.Collections.Generic/IEqualityComparerIn%7BT%7D.cs)
- [IReadOnlyStructEqualityComparer\<T>](./System.Collections.Generic/IReadOnlyStructEqualityComparer%7BT%7D.cs)
- [IComparerIn\<T>](./System.Collections.Generic/IComparerIn%7BT%7D.cs)
- [IReadOnlyStructComparer\<T>](./System.Collections.Generic/IReadOnlyStructComparer%7BT%7D.cs)

### Extensions
- [Collection\<T>](./System.Collections.Generic/Extensions/CollectionTExtensions.cs)
- [IEnumerable\<T>](./System.Collections.Generic/Extensions/IEnumerableTExtensions.cs)
- [IEnumerator\<T>](./System.Collections.Generic/Extensions/IEnumeratorTExtensions.cs)
- [List\<T>](./System.Collections.Generic/Extensions/ListTExtensions.cs)
- [Dictionary\<TKey, TValue>](./System.Collections.Generic/Extensions/DictionaryTKeyTValueExtensions.cs)
- [Queue\<T>](./System.Collections.Generic/Extensions/QueueTExtensions.cs)
- [Stack\<T>](./System.Collections.Generic/Extensions/StackTExtensions.cs)
- [HashSet\<T>](./System.Collections.Generic/Extensions/HashSetTExtensions.cs)

### Segment
- [ISegment\<T>](./System.Collections.Generic/Segments/Interfaces/ISegment.cs)
- [ISegmentReader\<T>](./System.Collections.Generic/Segments/Interfaces/ISegmentReader.cs)
- [ISegmentSource\<T>](./System.Collections.Generic/Segments/Interfaces/ISegmentSource.cs)
- [Segment\<T>](./System.Collections.Generic/Segments/Segment/Segment.cs)
- [Segment1\<T>](./System.Collections.Generic/Segments/Segment1.cs)
- [Array1Segment\<T>](./System.Collections.Generic/Segments/Array1Segment.cs)
- [ListSegment\<T>](./System.Collections.Generic/Segments/ListSegment.cs)
- [StringSegment](./System.Collections.Generic/Segments/StringSegment/StringSegment.cs)
- [SegmentReader\<TSegment, TValue>](./System.Collections.Generic/Segments/SegmentReader.cs)
- [Extensions](./System.Collections.Generic/Segments/Extensions)

### Read-Only Collection
- [ReadCollection\<T>](./System.Collections.Generic/ReadCollections/ReadCollection%7BT%7D.cs)
- [ReadDictionary\<TKey, TValue>](./System.Collections.Generic/ReadCollections/ReadDictionary%7BTKey%2CTValue%7D.cs)
- [ReadList\<T>](./System.Collections.Generic/ReadCollections/ReadList%7BT%7D.cs)
- [ReadHashSet\<T>](./System.Collections.Generic/ReadCollections/ReadHashSet%7BT%7D.cs)
- [Extensions](./System.Collections.Generic/ReadCollections/Extensions)

### Randomizer
- [Randomizer](./System.Collections.Generic/Randomizer/Randomizer.cs)
- [Randomizer.IRandom](./System.Collections.Generic/Randomizer/Randomizer.IRandom.cs)
- [Randomizer.ICache\<T>](./System.Collections.Generic/Randomizer/Randomizer.Cache%7BT%7D.cs)

### Misc.
- [Values\<T>](./System.Collections.Generic/Values.cs)
- [EnumValues\<T>](./System.Collections.Generic/EnumValues.cs)

## System.Collections.ArrayBased

- [ArrayList\<T>](./System.Collections.ArrayBased/ArrayList%7BT%7D.cs)
- [ArrayList\<T>.Collection](./System.Collections.ArrayBased/ArrayList%7BT%7D.Collection.cs)
- [ArrayDictionary\<TKey, TValue>](./System.Collections.ArrayBased/ArrayDictionary%7BTKey,TValue%7D.cs)
- [ArrayDictionary\<TKey, TValue>.Collection](./System.Collections.ArrayBased/ArrayDictionary%7BTKey,TValue%7D.Collection.cs)
- [ReadArrayList\<T>](./System.Collections.ArrayBased/ReadArrayList%7BT%7D.cs)
- [ReadArrayDictionary\<TKey, TValue>](./System.Collections.ArrayBased/ReadArrayDictionary%7BTKey,TValue%7D.cs)
- [RefReadArrayList\<T>](./System.Collections.ArrayBased/RefReadArrayList%7BT%7D.cs)
- [Extensions](./System.Collections.ArrayBased/Extensions)

## System.Collections.Pooling

- [IPool\<T>](./System.Collections.Pooling/IPool%7BT%7D.cs)
- [Pool\<T>](./System.Collections.Pooling/Pool%7BT%7D.cs)
- [IPoolProvider](./System.Collections.Pooling/IPoolProvider.cs)
- [IPoolProviderDecorator](./System.Collections.Pooling/IPoolProviderDecorator.cs)
- [Pool](./System.Collections.Pooling/Pool.cs)
- [Pool.DefaultProvider](./System.Collections.Pooling/Pool.DefaultProvider.cs)
- [DefaultProviderDecorator](./System.Collections.Pooling/DefaultProviderDecorator.cs)
- [Array1Pool\<T>](./System.Collections.Pooling/Pools/Array1Pool%7BT%7D.cs)
- [ListPool\<T>](./System.Collections.Pooling/Pools/ListPool%7BT%7D.cs)
- [DictionaryPool\<TKey, TValue>](./System.Collections.Pooling/Pools/DictionaryPool%7BTKey%2CTValue%7D.cs)
- [ArrayListPool\<T>](./System.Collections.Pooling/Pools/ArrayListPool%7BT%7D.cs)
- [ArrayDictionaryPool\<TKey, TValue>](./System.Collections.Pooling/Pools/ArrayDictionaryPool%7BTKey%2CTValue%7D.cs)
- [HashSetPool\<T>](./System.Collections.Pooling/Pools/HashSetPool%7BT%7D.cs)
- [QueuePool\<T>](./System.Collections.Pooling/Pools/QueuePool%7BT%7D.cs)
- [StackPool\<T>](./System.Collections.Pooling/Pools/StackPool%7BT%7D.cs)

## System.Collections.Pooling.Concurrent
- [ConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/ConcurrentPool%7BT%7D.cs)
- [IConcurrentPoolProvider](./System.Collections.Pooling.Concurrent/IConcurrentPoolProvider.cs)
- [IConcurrentPoolProviderDecorator](./System.Collections.Pooling.Concurrent/IConcurrentPoolProviderDecorator.cs)
- [ConcurrentPool](./System.Collections.Pooling.Concurrent/ConcurrentPool.cs)
- [ConcurrentPool.DefaultProvider](./System.Collections.Pooling.Concurrent/ConcurrentPool.DefaultProvider.cs)
- [DefaultConcurrentProviderDecorator](./System.Collections.Pooling.Concurrent/DefaultConcurrentProviderDecorator.cs)
- [Array1ConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/Pools/Array1ConcurrentPool%7BT%7D.cs)
- [ListConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/Pools/ListConcurrentPool%7BT%7D.cs)
- [DictionaryConcurrentPool\<TKey, TValue>](./System.Collections.Pooling.Concurrent/Pools/DictionaryConcurrentPool%7BTKey%2CTValue%7D.cs)
- [ArrayListConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/Pools/ArrayListConcurrentPool%7BT%7D.cs)
- [ArrayDictionaryConcurrentPool\<TKey, TValue>](./System.Collections.Pooling.Concurrent/Pools/ArrayDictionaryConcurrentPool%7BTKey%2CTValue%7D.cs)
- [HashSetConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/Pools/HashSetConcurrentPool%7BT%7D.cs)
- [QueueConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/Pools/QueueConcurrentPool%7BT%7D.cs)
- [StackConcurrentPool\<T>](./System.Collections.Pooling.Concurrent/Pools/StackConcurrentPool%7BT%7D.cs)
- [ConcurrentBagPool\<T>](./System.Collections.Pooling.Concurrent/Pools/ConcurrentBagPool%7BT%7D.cs)
- [ConcurrentDictionaryPool\<TKey, TValue>](./System.Collections.Pooling.Concurrent/Pools/ConcurrentDictionaryPool%7BTKey%2CTValue%7D.cs)
- [ConcurrentQueuePool\<T>](./System.Collections.Pooling.Concurrent/Pools/ConcurrentQueuePool%7BT%7D.cs)
- [ConcurrentStackPool\<T>](./System.Collections.Pooling.Concurrent/Pools/ConcurrentStackPool%7BT%7D.cs)

## System.Fluent

- [ObjectExtensions.FluentDelegates](./System.Fluent/Extensions/ObjectExtensions.FluentDelegates.cs)

## System.Grid

- [GridIndex](./System.Grid/GridIndex.cs)
- [SGridIndex](./System.Grid/SGridIndex.cs)
- [GridIndexRange](./System.Grid/GridIndexRange.cs)
- [GridIndexRange.Enumerator](./System.Grid/GridIndexRange.Enumerator.cs)
- [GridRange](./System.Grid/GridRange.cs)
- [GridRange.Enumerator](./System.Grid/GridRange.Enumerator.cs)
- [GridSize](./System.Grid/GridSize.cs)
- [ClampedGridSize](./System.Grid/ClampedGridSize.cs)
- [GridPartitioner](./System.Grid/GridPartitioner.cs)
- [ClampedGridPartitioner](./System.Grid/ClampedGridPartitioner.cs)
- [GridValue\<T>](./System.Grid/GridValue%7BT%7D.cs)
- [IGridValues\<T>](./System.Grid/IGridValues%7BT%7D.cs)
- [IGridValueEnumerator\<T>](./System.Grid/IGridValueEnumerator%7BT%7D.cs)
- [IGridIndexedValues\<T>](./System.Grid/IGridIndexedValues%7BT%7D.cs)
- [IGridIndexedValueEnumerator\<T>](./System.Grid/IGridIndexedValueEnumerator%7BT%7D.cs)
- [IReadOnlyGrid\<T>](./System.Grid/IReadOnlyGrid%7BT%7D.cs)
- [IGrid\<T>](./System.Grid/IGrid%7BT%7D.cs)
- [Grid\<T>](./System.Grid/Grid%7BT%7D.cs)
- [Grid\<T>.GridValues](./System.Grid/Grid%7BT%7D.GridValues.cs)
- [Grid\<T>.GridIndexedValues](./System.Grid/Grid%7BT%7D.GridIndexedValues.cs)
- [ReadGrid\<T>](./System.Grid/ReadGrid%7BT%7D.cs)

## System.Grid.ArrayBased

- [ArrayGrid\<T>](./System.Grid.ArrayBased/ArrayGrid%7BT%7D.cs)
- [ArrayGrid\<T>.GridValues](./System.Grid.ArrayBased/ArrayGrid%7BT%7D.GridValues.cs)
- [ArrayGrid\<T>.GridIndexedValues](./System.Grid.ArrayBased/ArrayGrid%7BT%7D.GridIndexedValues.cs)
- [ReadArrayGrid\<T>](./System.Grid.ArrayBased/ReadArrayGrid%7BT%7D.cs)

## System.Table

- [IEntry](./System.Table/IEntry.cs)
- [IGetId](./System.Table/IGetId.cs)
- [ITable](./System.Table/ITable.cs)
- [Table](./System.Table/Table.cs)
- [ReadEntry](./System.Table/ReadEntry.cs)
- [ReadTable](./System.Table/ReadTable.cs)

## UnityEngine

### Collection
- [SerializableDictionary\<TKey, TValue>](./UnityEngine/SerializableDictionary.cs)

### Singleton
- [SingletonBehaviour\<T>](./UnityEngine/SingletonBehaviour.cs)

### Extensions
- [BoundsExtensions](./UnityEngine/Extensions/BoundsExtensions.cs)
- [ColorExtensions](./UnityEngine/Extensions/ColorExtensions.cs)
- [MatrixExtenions](./UnityEngine/Extensions/MatrixExtensions.cs)
- [QuaternionExtensions](./UnityEngine/Extensions/QuaternionExtensions.cs)
- [RangeExtensions](./UnityEngine/Extensions/RangeExtensions.cs)
- [RayExtensions](./UnityEngine/Extensions/RayExtensions.cs)
- [RectExtensions](./UnityEngine/Extensions/RectExtensions.cs)
- [ResolutionExtensions](./UnityEngine/Extensions/ResolutionExtensions.cs)
- [VectorExtensions](./UnityEngine/Extensions/VectorExtensions.cs)

### Misc.
- [HSBColor](./UnityEngine/HSBColor.cs)
- [LEBColor](./UnityEngine/LEBColor.cs)
- [Offset](./UnityEngine/Offset.cs)
- [OffsetInt](./UnityEngine/OffsetInt.cs)
- [ScreenResolution](./UnityEngine/ScreenResolution.cs)
- [Size](./UnityEngine/Size.cs)
- [SizeInt](./UnityEngine/SizeInt.cs)
- [SingleLayer](./UnityEngine/SingleLayer.cs)
- [SortingLayerId](./UnityEngine/SortingLayerId.cs)
- [GridVector](./UnityEngine/GridVector.cs)
- [SGridVector](./UnityEngine/SGridVector.cs)

## Unity.Collections

- [ReadNativeArray\<T>](./Unity.Collections/Segments/NativeArray/ReadNativeArray%7BT%7D.cs)
- [ReadNativeSlice\<T>](./Unity.Collections/Segments/NativeSlice/ReadNativeSlice%7BT%7D.cs)
- [NativeArraySegment\<T>](./Unity.Collections/Segments/NativeArray/NativeArraySegment.cs)
- [NativeSliceSegment\<T>](./Unity.Collections/Segments/NativeSlice/NativeSliceSegment.cs)
- [Extensions](./Unity.Collections/Segments/Extensions/NativeSegmentExtensions.cs)

## UnityEditor
- [SingleLayerDrawer](./Editor/UnityEngine/SingleLayerDrawer.cs)
- [SortingLayerIdDrawer](./Editor/UnityEngine/SortingLayerIdDrawer.cs)
- [GridVectorDrawer](./Editor/UnityEngine/GridVectorDrawer.cs)
