-- Determine if a contiguous subsequence exists that sums to a 
-- specified value

import Debug.Trace


data TestCase = Test [Int] Int Bool deriving (Show)

tests :: [TestCase]
tests = [
   Test [23, 5, 4, 7, 2, 11] 20 True,
   Test [1, 3, 5, 23, 2] 8  True,
   Test [1, 3, 5, 23, 2] 7  False,
   Test [1, 3, 5, 23, 2] 23  True,
   Test [1, 3, 5, 23] 23 True,
   Test [0, 1, 3, 5, 23] 23 True,
   Test [1] 7 False,
   Test [1] 1 True,
   Test [22] 1 False
   ]

main :: IO()
main = sequence_ . map (\test -> putStrLn . (++) (show test ++ " -> ") . runTestCase $ test) $ tests


runTestCase :: TestCase -> String
runTestCase (Test arr num expected) = if testPassed then "PASSED" else "FAILED"
    where testPassed = (subsequenceFound arr num) == expected


subsequenceFound :: [Int] -> Int -> Bool
subsequenceFound arr num = subsequenceFound' [] arr 0 num


subsequenceFound' :: [Int] -> [Int] -> Int -> Int -> Bool
subsequenceFound' buffer remaining currentSum expectedSum 
    | currentSum == expectedSum = True
    | currentSum > expectedSum  = subsequenceFound' bt remaining (currentSum - bh) expectedSum
    | remaining == []           = False
    | otherwise                 = subsequenceFound' (buffer ++ [rh]) rt (currentSum + rh) expectedSum
    where rh = head remaining
          rt = tail remaining
          bh = head buffer
          bt = tail buffer

