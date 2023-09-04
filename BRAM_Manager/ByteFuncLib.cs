using System;
using System.Linq;

// This library contains functions related to byte operations, as well as PCE-specifc binary data
internal class ByteFuncLib
{
	public static int maxBRAMSize = 2048;
	public static int memoryOffset = 32768;
	public static int headerSize = 16;
	public static int entryHeaderSize = 16;
	public static byte[] newFileBytes = { 0x48, 0x55, 0x42, 0x4D, 0x00, 0x88, 0x10, 0x80 };

	// Slightly inaccurate character map, inaccuracies are to ensure no duplicates for accurate encoding
	public static char[] characterMap = new char[] {
	' ', '▁', '▂', '▃', '▄', '▅', '▆', '▇', '█', '♤', '♡', '◇', '♧', '○', '●', '/',
	'\\', '円', '年', '月', '日', '時', '分', '秒', '◢', '◣', '◥', '◤', '→', '←', '↑', '↓',
	' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '／',
	'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
	'@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
	'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '￥', ']', '^', '_',
	' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
	'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '¦', '}', '~', '☓',
	' ', '。', '「', '」', '、', '・', 'を', 'ぁ', '╭', '─', '╮', '│', '｜', '╰', '━', '╯',
	'ー', 'あ', 'い', 'う', 'え', 'お', 'か', 'き', 'く', 'け', 'こ', 'さ', 'し', 'す', 'せ', 'そ',
	'　', '｡', '｢', '｣', '､', '･', 'ヲ', 'ァ', '┬', '|', '┴', 'ォ', 'ャ', 'ュ', 'ョ', 'ッ',
	'－', 'ア', 'イ', 'ウ', 'エ', 'オ', 'カ', 'キ', 'ク', 'ケ', 'コ', 'サ', 'シ', 'ス', 'セ', 'ソ',
	'タ', 'チ', 'ツ', 'テ', 'ト', 'ナ', 'ニ', 'ヌ', 'ネ', 'ノ', 'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'マ',
	'ミ', 'ム', 'メ', 'モ', 'ヤ', 'ユ', 'ヨ', 'ラ', 'リ', 'ル', 'レ', 'ロ', 'ワ', 'ン', '゛', '゜',
	'た', 'ち', 'つ', 'て', 'と', 'な', 'に', 'ぬ', 'ね', 'の', 'は', 'ひ', 'ふ', 'へ', 'ほ', 'ま',
	'み', 'む', 'め', 'も', 'や', 'ゆ', 'よ', 'ら', 'り', 'る', 'れ', 'ろ', 'わ', 'ん', '゙', '©'};

	// Binary data handling ----------------------------

	// Returns bytes from a byte array, given a range
	public static byte[] ReadBytes(byte[] data, int from, int to)
	{
		byte[] result = new byte[to - from];

		int index = 0;
		for (int i = from; i < to; i++)
		{
			result[index] = data[i];
			index++;
		}

		return result;
	}

	// Converts bytes into a human-readable form following the PC Engine character map
	public static string DecodeBytes(byte[] data)
	{
		string result = string.Empty;

		foreach (byte Byte in data)
		{
			if (characterMap.Length > Byte)
			{
				result += characterMap[Byte];
			}
		}

		return result;
	}

	// Converts a string into valid bytes following the PC Engine character map
	public static byte[] EncodeString(string data)
	{
		byte[] result = new byte[data.Length];

		for (int i = 0; i < data.Length; i++)
		{
			char Char = data[i];
			if (characterMap.Contains(Char))
			{
				result[i] = Convert.ToByte(Array.IndexOf(characterMap, Char));
			}
		}

		return result;
	}

	// Writes bytes to a byte array, given a starting location and a length
	public static void WriteBytes(byte[] input, byte[] dest, int startLoc, int length)
	{
		int index = 0;
		for (int i = startLoc; i < startLoc + length; i++)
		{
			if (dest.Length > i && input.Length > index)
			{
				dest[i] = input[index];
				index++;
			}
		}
	}

	// Calculates the checksum for a given BRAM entry
	public static byte[] CalculateChecksum(byte[] data, int length)
	{
		byte[] result = new byte[2];
		int calculationStartsAt = 4;
		int dataSize = length - calculationStartsAt;
		byte[] newData = new byte[dataSize];

		// get data bytes minus first four bytes
		int index = 0;
		for (int i = calculationStartsAt; i < length; i++)
		{
			newData[index] = data[i];
			index++;
		}

		// checksum is the sum of all bytes
		int sum = 0;
		foreach (byte Byte in newData)
		{
			sum += Byte;
		}

		// stored checksum is the negation of the checksum
		int checksum = sum * -1;
		result = BitConverter.GetBytes(checksum);

		return result;
	}
}