using System.Collections.Generic;
using System.Linq;

namespace AtCoder.Libraries
{
    public class BackTracking
    {
		/// <summary>
		/// Returns subsets. E.x. {1,2,3} => [{}{1}{2}{3},{1,2},{1,3},{2,3},{1,2,3}]
		/// </summary>
		/// <param name="nums"></param>
		/// <returns></returns>
		public IList<IList<int>> GetSubsets(int[] nums)
		{
			for (int i = 0; i <= nums.Length; i++)
			{
				SubSetsRecursive(nums, new List<int>(), 0, i);
			}

			return _output;
		}

		void SubSetsRecursive(int[] nums, List<int> temp, int first, int num)
		{
			if (temp.Count == num)
			{
				_output.Add(temp.ToList());
				return;
			}

			for (int i = first; i < nums.Length; i++)
			{
				temp.Add(nums[i]);
				SubSetsRecursive(nums, temp, i + 1, num);
				temp.RemoveAt(temp.Count - 1);
			}
		}

		/// <summary>
		/// Returns permutations. E.x. {1,2,3} => [{1,2,3},{1,3,2},{2,1,3},{2,3,1},{3,1,2},{3,2,1}]
		/// </summary>
		/// <param name="nums"></param>
		/// <returns></returns>
		public IList<IList<int>> GetPermute(int[] nums)
		{
			PermuteRecursive(nums, 0);
			return _output;
		}

		void PermuteRecursive(int[] nums, int first)
		{
			if (first == nums.Length)
			{
				_output.Add(nums.ToList());
			}

			for (int i = first; i < nums.Length; i++)
			{
				(nums[first], nums[i]) = (nums[i], nums[first]);
				PermuteRecursive(nums, first + 1);
				(nums[first], nums[i]) = (nums[i], nums[first]);
			}
		}

		/// <summary>
		/// Returns boolean sets. E.x. (2) => [{false,false},{false,true},{true,false},{true,true}]
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public IList<IList<bool>> GetBoolSets(int num)
		{
			GetBoolSetsRecursive(Enumerable.Repeat(false, num).ToArray(), 0);
			return _outputBool;
		}

		void GetBoolSetsRecursive(bool[] temp, int first)
		{
			if (first == temp.Length)
			{
				_outputBool.Add(temp.ToList());
				return;
			}

			temp[first] = true;
			GetBoolSetsRecursive(temp, first + 1);
			temp[first] = false;
			GetBoolSetsRecursive(temp, first + 1);
		}

		private IList<IList<bool>> _outputBool = new List<IList<bool>>();
		public IList<IList<int>> _output = new List<IList<int>>();
	}
}
